using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Module.ArtworksSearcher.ImagesProviders
{
    public interface IImagesProvidersFactory
    {
        IAsyncEnumerable<BitmapImage> SearchInOsuDirectory(string query);
        IAsyncEnumerable<BitmapImage> SearchInGoogle(string query);
    }
}