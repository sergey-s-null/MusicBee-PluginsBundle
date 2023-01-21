using System.Collections.Generic;
using System.Threading;
using System.Windows.Media.Imaging;
using Module.ArtworksSearcher.Factories;

namespace Module.ArtworksSearcher.ImagesProviders
{
    public sealed class GoogleImagesProvider : IAsyncEnumerable<BitmapImage>
    {
        private readonly string _query;

        private readonly GoogleImagesEnumeratorFactory _googleImagesEnumeratorFactory;

        public GoogleImagesProvider(
            string query,
            // DI
            GoogleImagesEnumeratorFactory googleImagesEnumeratorFactory)
        {
            _query = query;

            _googleImagesEnumeratorFactory = googleImagesEnumeratorFactory;
        }

        public IAsyncEnumerator<BitmapImage> GetAsyncEnumerator(CancellationToken cancellationToken)
        {
            return _googleImagesEnumeratorFactory(_query, cancellationToken);
        }
    }
}