using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ArtworksSearcher.ImagesProviders
{
    public interface IImagesProvider
    {
        IEnumerable<BitmapImage> GetImagesIter(string query);
        IAsyncEnumerator<BitmapImage> GetAsyncEnumerator(string query);
    }
}
