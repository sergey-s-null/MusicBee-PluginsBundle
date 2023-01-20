using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Media.Imaging;
using Module.ArtworksSearcher.Factories;

namespace Module.ArtworksSearcher.ImagesProviders
{
    public sealed class GoogleImagesProvider : IAsyncEnumerable<BitmapImage>
    {
        private readonly string _query;

        private readonly IGoogleImagesEnumeratorFactory _googleImagesEnumeratorFactory;
        
        public GoogleImagesProvider(
            string query,
            // DI
            IGoogleImagesEnumeratorFactory googleImagesEnumeratorFactory)
        {
            _query = query;

            _googleImagesEnumeratorFactory = googleImagesEnumeratorFactory;
        }

        public IAsyncEnumerator<BitmapImage> GetAsyncEnumerator(CancellationToken cancellationToken)
        {
            return _googleImagesEnumeratorFactory.Create(_query, cancellationToken);
        }
    }
}
