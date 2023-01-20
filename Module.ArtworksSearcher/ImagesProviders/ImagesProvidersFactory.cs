using System.Collections.Generic;
using System.Windows.Media.Imaging;
using Module.ArtworksSearcher.Factories;
using Module.ArtworksSearcher.Settings;

namespace Module.ArtworksSearcher.ImagesProviders
{
    public sealed class ImagesProvidersFactory : IImagesProvidersFactory
    {
        private readonly IArtworksSearcherSettings _settings;
        private readonly IGoogleImagesEnumeratorFactory _googleImagesEnumeratorFactory;
        
        public ImagesProvidersFactory(
            IArtworksSearcherSettings settings,
            IGoogleImagesEnumeratorFactory googleImagesEnumeratorFactory)
        {
            _settings = settings;
            _googleImagesEnumeratorFactory = googleImagesEnumeratorFactory;
        }
        
        public IAsyncEnumerable<BitmapImage> SearchInOsuDirectory(string query)
        {
            return new OsuImagesProvider(query, _settings);
        }

        public IAsyncEnumerable<BitmapImage> SearchInGoogle(string query)
        {
            return new GoogleImagesProvider(query, _googleImagesEnumeratorFactory);
        }
    }
}