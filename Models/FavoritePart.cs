using Orchard.ContentManagement;

namespace NGM.Favorite.Models {
    public class FavoritePart : ContentPart {
        public bool ShowVoter { get; set; }

        public bool IsFavorite { get; set; }

        public double NumberOfFavorites { get; set; }
    }
}