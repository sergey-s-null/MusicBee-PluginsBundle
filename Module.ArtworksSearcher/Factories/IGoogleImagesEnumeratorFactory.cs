using System.Threading;
using Module.ArtworksSearcher.ImagesProviders;

namespace Module.ArtworksSearcher.Factories
{
    public interface IGoogleImagesEnumeratorFactory
    {
        GoogleImagesAsyncEnumerator Create(string query, CancellationToken cancellationToken);
    }
}