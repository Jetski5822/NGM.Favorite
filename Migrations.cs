using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Localization;

namespace NGM.Favorite {
    public class Migrations : DataMigrationImpl {
        public Migrations() {
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public int Create() {
            ContentDefinitionManager.AlterPartDefinition("FavoritePart", builder => builder
                .WithDescription(T("Allows you to set a content part as a favorite.").Text)
                .Attachable());

            return 2;
        }

        public int UpdateFrom1() {
            ContentDefinitionManager.AlterPartDefinition("FavoritePart", builder => builder
                .WithDescription(T("Allows you to set a content part as a favorite.").Text));

            return 2;
        }
    }
}