using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Module.ArtworksSearcher.ImagesProviders;
using Module.Mvvm.Extension;
using PropertyChanged;

namespace Module.ArtworksSearcher.GUI.SearchWindow;

[AddINotifyPropertyChangedInterface]
public class SearchWindowVM
{
    #region Bindings

    public string Artist { get; set; } = "";
    public string Title { get; set; } = "";
    public string SearchText { get; set; } = "";

    private RelayCommand? _resetSearchTextCmd;

    public RelayCommand ResetSearchTextCmd
        => _resetSearchTextCmd ??= new RelayCommand(_ => ResetSearchText());

    private RelayCommand? _selectImageCmd;

    public RelayCommand SelectImageCmd
        => _selectImageCmd ??= new RelayCommand(arg =>
        {
            if (arg is ImageVM imageVM)
            {
                SelectImage(imageVM);
            }
        });

    public ImageVM? SelectedResult { get; set; }

    public ImagesProviderVM[] ImagesProviders { get; }

    public ImagesProviderVM SelectedProvider { get; set; }

    private RelayCommand? _searchCmd;

    public RelayCommand SearchCmd
        => _searchCmd ??= new RelayCommand(async _ => await Search());

    private RelayCommand? _nextImageCmd;

    public RelayCommand NextImageCmd
        => _nextImageCmd ??= new RelayCommand(async _ => await NextImage());

    #endregion

    public event EventHandler? OnClearResults;

    // TODO MB Dispose
    private readonly Mutex _searchMutex = new();
    private readonly Mutex _nextImageMutex = new();

    public SearchWindowVM(
        IImagesProvidersFactory imagesProvidersFactory)
    {
        ImagesProviders = new ImagesProviderVM[]
        {
            new("Google",
                query => imagesProvidersFactory
                    .SearchInGoogle(query)
                    .GetAsyncEnumerator(default)), // TODO token
            new("Osu!dir",
                query => imagesProvidersFactory
                    .SearchInOsuDirectory(query)
                    .GetAsyncEnumerator(default)) // TODO token
        };

        SelectedProvider = ImagesProviders[0];
    }

    public void ResetSearchText()
    {
        SearchText = $"{Artist} - {Title}";
    }

    private void SelectImage(ImageVM imageToSelect)
    {
        SelectedResult = imageToSelect;
    }

    private async Task Search()
    {
        if (!_searchMutex.WaitOne(0))
        {
            return;
        }

        try
        {
            SelectedResult = null;
            OnClearResults?.Invoke(this, EventArgs.Empty);
            SelectedProvider.StartSearch(SearchText);
            GC.Collect();
            await NextImage();
        }
        finally
        {
            _searchMutex.ReleaseMutex();
        }
    }

    private async Task NextImage()
    {
        if (!_nextImageMutex.WaitOne(0))
        {
            return;
        }

        try
        {
            await SelectedProvider.MoveNextAsync();
        }
        finally
        {
            _nextImageMutex.ReleaseMutex();
        }
    }

    public bool TryGetImageData(out byte[]? imageData)
    {
        if (SelectedResult is null)
        {
            MessageBox.Show("Selected is none.");
            imageData = null;
            return false;
        }

        using var memoryStream = new MemoryStream();
        BitmapEncoder encoder = new JpegBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(SelectedResult.Image));
        encoder.Save(memoryStream);
        imageData = memoryStream.ToArray();
        return true;
    }
}