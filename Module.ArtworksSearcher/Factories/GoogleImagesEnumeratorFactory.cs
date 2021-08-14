using Module.ArtworksSearcher.ImagesProviders;
using Module.ArtworksSearcher.Settings;

namespace Module.ArtworksSearcher.Factories
{
    // TODO мб как-то все-таки можно через интерфейс и ToFactory
    public class GoogleImagesEnumeratorFactory : IGoogleImagesEnumeratorFactory
    {
        private readonly IArtworksSearcherSettings _settings;
        
        public GoogleImagesEnumeratorFactory(IArtworksSearcherSettings settings)
        {
            _settings = settings;
        }
        
        public GoogleImagesEnumerator Create(string query)
        {
            return new(query, _settings);
        }
    }
}