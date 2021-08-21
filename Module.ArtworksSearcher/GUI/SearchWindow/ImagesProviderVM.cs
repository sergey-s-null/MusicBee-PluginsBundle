using Module.ArtworksSearcher.ImagesProviders;
using PropertyChanged;

namespace Module.ArtworksSearcher.GUI.SearchWindow
{
    [AddINotifyPropertyChangedInterface]
    public class ImagesProviderVM
    {
        public IImagesProvider Provider { get; set; }
        public string Name { get; set; }
    }
}
