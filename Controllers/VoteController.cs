using System.Linq;
using System.Web.Mvc;
using Contrib.Voting.Services;
using NGM.Favorite.Models;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard.Mvc.Extensions;

namespace NGM.Favorite.Controllers {
    public class VoteController  : Controller {
        private readonly IOrchardServices _orchardServices;
        private readonly IContentManager _contentManager;
        private readonly IVotingService _votingService;

        public VoteController(IOrchardServices orchardServices, IContentManager contentManager, IVotingService votingService) {
            _orchardServices = orchardServices;
            _contentManager = contentManager;
            _votingService = votingService;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        [HttpPost]
        public ActionResult Apply(int contentId, string returnUrl) {
            var content = _contentManager.Get(contentId);
            if (content == null || !content.Has<FavoritePart>() || !content.As<FavoritePart>().ShowVoter)
                return this.RedirectLocal(returnUrl, "~/");

            var currentUser = _orchardServices.WorkContext.CurrentUser;
            if (currentUser == null)
                return this.RedirectLocal(returnUrl, "~/");

            var currentVote = _votingService.Get(vote => 
                vote.Username == currentUser.UserName && 
                vote.ContentItemRecord == content.Record && 
                vote.Dimension == Constants.Dimension).FirstOrDefault();

            if (currentVote != null) {
                _votingService.RemoveVote(currentVote);
            }
            else {
                _votingService.Vote(content, currentUser.UserName, HttpContext.Request.UserHostAddress, 1, Constants.Dimension);
            }

            return this.RedirectLocal(returnUrl, "~/");
        }
    }
}