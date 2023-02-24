using System.Windows.Media.Imaging;

namespace Module.ArtworksSearcher.Delegates;

public delegate IAsyncEnumerator<BitmapImage> GetImagesAsyncEnumeratorDelegate(string query);