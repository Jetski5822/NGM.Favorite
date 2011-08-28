using System.Linq;
using Contrib.Voting.Services;
using NGM.Favorite.Models;
using Orchard;
using Orchard.ContentManagement.Drivers;

namespace NGM.Favorite.Drivers {
    public class FavoritePartDriver : ContentPartDriver<FavoritePart> {
        private readonly IOrchardServices _orchardServices;
        private readonly IVotingService _votingService;

        public FavoritePartDriver(IOrchardServices orchardServices, IVotingService votingService) {
            _orchardServices = orchardServices;
            _votingService = votingService;
        }

        protected override DriverResult Display(FavoritePart part, string displayType, dynamic shapeHelper) {
            if (!part.ShowVoter)
                return null;

            var displayPart = BuildVoteUpDown(part);

            return Combined(
                ContentShape(
                    "Parts_Favorite",
                        () => shapeHelper.Parts_Favorite(displayPart))
                );
        }

        private FavoritePart BuildVoteUpDown(FavoritePart part) {
            var currentUser = _orchardServices.WorkContext.CurrentUser;

            if (currentUser != null) {
                var resultRecord = _votingService.GetResult(part.ContentItem.Id, "sum", "Favorite");

                part.IsFavorite = (resultRecord != null && resultRecord.Value > 0.0);
                part.NumberOfFavorites = _votingService.Get(vote => vote.Username == currentUser.UserName && vote.Dimension == "Favorite").Sum(o => o.Value);
            }

            return part;
        }
    }
}