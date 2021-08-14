using Module.ArtworksSearcher.ImagesProviders;

namespace Module.ArtworksSearcher.Factories
{
    public interface IGoogleImagesEnumeratorFactory
    {
        GoogleImagesEnumerator Create(string query);
    }
}