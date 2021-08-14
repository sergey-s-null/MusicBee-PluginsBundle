using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace ArtworksSearcher.ImagesProviders
{
    public interface IImagesProvider
    {
        IEnumerable<BitmapImage> GetImagesIter(string query);
        IAsyncEnumerator<BitmapImage> GetAsyncEnumerator(string query);
    }
}
