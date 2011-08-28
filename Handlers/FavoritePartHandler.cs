using NGM.Favorite.Models;
using NGM.Favorite.Settings;
using Orchard.ContentManagement.Handlers;

namespace NGM.Favorite.Handlers {
    public class FavoritePartHandler : ContentHandler {
        public FavoritePartHandler() {
            OnInitializing<FavoritePart>((context, part) => {
                                               part.ShowVoter = part.Settings.GetModel<FavoriteTypePartSettings>().ShowVoter;
                                           });
        }
    }
}