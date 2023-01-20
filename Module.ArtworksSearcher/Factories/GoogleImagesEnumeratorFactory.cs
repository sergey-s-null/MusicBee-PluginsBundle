using System.Threading;
using Module.ArtworksSearcher.ImagesProviders;

namespace Module.ArtworksSearcher.Factories
{
    public delegate GoogleImagesAsyncEnumerator GoogleImagesEnumeratorFactory(string query, CancellationToken cancellationToken);
}