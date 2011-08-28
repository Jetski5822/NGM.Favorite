using System.Collections.Generic;
using Orchard.ContentManagement;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.ContentManagement.MetaData.Models;
using Orchard.ContentManagement.ViewModels;

namespace NGM.Favorite.Settings {
    public class FavoriteTypePartSettings {
        private bool? _showVoter;
        public bool ShowVoter {
            get {
                if (_showVoter == null)
                    _showVoter = true;
                return (bool)_showVoter;
            }
            set { _showVoter = value; }
        }
    }

    public class ContainerSettingsHooks : ContentDefinitionEditorEventsBase {
        public override IEnumerable<TemplateViewModel> TypePartEditor(ContentTypePartDefinition definition) {
            if (definition.PartDefinition.Name != "FavoritePart")
                yield break;

            var model = definition.Settings.GetModel<FavoriteTypePartSettings>();

            yield return DefinitionTemplate(model);
        }

        public override IEnumerable<TemplateViewModel> TypePartEditorUpdate(ContentTypePartDefinitionBuilder builder, IUpdateModel updateModel) {
            if (builder.Name != "FavoritePart")
                yield break;

            var model = new FavoriteTypePartSettings();
            updateModel.TryUpdateModel(model, "FavoriteTypePartSettings", null, null);
            builder.WithSetting("FavoriteTypePartSettings.ShowVoter", model.ShowVoter.ToString());

            yield return DefinitionTemplate(model);
        }
    }
}