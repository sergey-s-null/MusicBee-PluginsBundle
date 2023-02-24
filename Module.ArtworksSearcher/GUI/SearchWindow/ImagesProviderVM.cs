using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using Module.ArtworksSearcher.Delegates;

namespace Module.ArtworksSearcher.GUI.SearchWindow;

public sealed class ImagesProviderVM
{
    private readonly GetImagesAsyncEnumeratorDelegate _getImagesEnumerator;
    private IAsyncEnumerator<BitmapImage>? _imagesEnumerator;
        
    public string ProviderName { get; }
    public ObservableCollection<ImageVM> Images { get; } = new();

    public ImagesProviderVM(string providerName, GetImagesAsyncEnumeratorDelegate getImagesEnumerator)
    {
        _getImagesEnumerator = getImagesEnumerator;
            
        ProviderName = providerName;
    }
        
    public void StartSearch(string query)
    {
        Images.Clear();
                
        _imagesEnumerator = _getImagesEnumerator(query);
    }
        
    public async Task MoveNextAsync()
    {
        if (_imagesEnumerator is null)
        {
            return;
        }

        if (await _imagesEnumerator.MoveNextAsync())
        {
            Images.Add(new ImageVM(Images.Count + 1, _imagesEnumerator.Current));
        }
    }
}