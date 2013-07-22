using System.Linq;
using Contrib.Voting.Services;
using NGM.Favorite.Models;
using Orchard;
using Orchard.ContentManagement.Drivers;
using Orchard.Security;

namespace NGM.Favorite.Drivers {
    public class FavoritePartDriver : ContentPartDriver<FavoritePart> {
        private readonly IAuthenticationService _authenticationService;
        private readonly IVotingService _votingService;

        public FavoritePartDriver(IAuthenticationService authenticationService, IVotingService votingService) {
            _authenticationService = authenticationService;
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
            var currentUser = _authenticationService.GetAuthenticatedUser();

            if (currentUser != null) {
                var resultRecord = _votingService.GetResult(part.ContentItem.Id, "sum", Constants.Dimension);

                part.IsFavorite = (resultRecord != null && resultRecord.Value > 0.0);
                part.NumberOfFavorites = _votingService.Get(vote => vote.Username == currentUser.UserName && vote.Dimension == Constants.Dimension).Sum(o => o.Value);
            }

            return part;
        }
    }
}