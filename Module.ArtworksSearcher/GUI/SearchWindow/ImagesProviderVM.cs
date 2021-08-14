using Module.ArtworksSearcher.ImagesProviders;
using Root.MVVM;

namespace Module.ArtworksSearcher.GUI.SearchWindow
{
    public class ImagesProviderVM : BaseViewModel
    {
        #region Bindings

        private IImagesProvider _provider;
        public IImagesProvider Provider
        {
            get => _provider;
            set
            {
                _provider = value;
                NotifyPropChanged(nameof(Provider));
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropChanged(nameof(Name));
            }
        }

        #endregion
    }
}
