using System.Collections.Generic;
using System.Windows.Media.Imaging;
using Root.Abstractions;

namespace Module.ArtworksSearcher.ImagesProviders
{
    public interface IImagesProvider
    {
        IEnumerable<BitmapImage> GetImagesIter(string query);
        IAsyncEnumerator<BitmapImage> GetAsyncEnumerator(string query);
    }
}
