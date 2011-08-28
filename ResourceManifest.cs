using Orchard.UI.Resources;

namespace NGM.Favorite {
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();
            manifest.DefineStyle("NGM.Favorite").SetUrl("NGM.Favorite.css");
        }
    }
}
