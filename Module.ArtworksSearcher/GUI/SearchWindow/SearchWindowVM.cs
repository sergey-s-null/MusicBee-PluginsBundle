using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Module.ArtworksSearcher.ImagesProviders;
using PropertyChanged;
using Root.Abstractions;
using Root.MVVM;

namespace Module.ArtworksSearcher.GUI.SearchWindow
{
    [AddINotifyPropertyChangedInterface]
    public class SearchWindowVM
    {
        #region Bindings

        public string Artist { get; set; }
        public string Title { get; set; }
        public string SearchText { get; set; }

        private RelayCommand _resetSearchTextCmd;
        public RelayCommand ResetSearchTextCmd
            => _resetSearchTextCmd ??= new RelayCommand(_ => ResetSearchText());

        private RelayCommand _selectImageCmd;
        public RelayCommand SelectImageCmd
            => _selectImageCmd ??= new RelayCommand(arg => SelectImage(arg));

        private ObservableCollection<ImageVM> _searchResults;
        public ObservableCollection<ImageVM> SearchResults =>
            _searchResults ??= new ObservableCollection<ImageVM>();

        public ImageVM SelectedResult { get; set; }

        public ImagesProviderVM[] ImagesProviders { get; }

        public ImagesProviderVM SelectedProvider { get; set; }

        private RelayCommand _searchCmd;
        public RelayCommand SearchCmd
            => _searchCmd ??= new RelayCommand(_ => Search());

        private RelayCommand _nextImageCmd;
        public RelayCommand NextImageCmd
            => _nextImageCmd ??= new RelayCommand(_ => NextImage());

        #endregion

        private IAsyncEnumerator<BitmapImage> _imagesAsyncEnumerator;
        public event EventHandler OnClearResults;

        public SearchWindowVM(
            GoogleImagesProvider googleImagesProvider,// TODO возможно есть более красивый способ создание
            OsuImagesProvider osuImagesProvider)
        {
            ImagesProviders = new ImagesProviderVM[]
            {
                new()
                {
                    Provider = googleImagesProvider,
                    Name = "Google"
                },
                new()
                {
                    Provider = osuImagesProvider,
                    Name = "Osu!dir"
                }
            };

            SelectedProvider = ImagesProviders[0];
        }

        public void ResetSearchText()
        {
            SearchText = $"{Artist} - {Title}";
        }

        private void SelectImage(object arg)
        {
            if (arg is ImageVM selectedResult)
            {
                SelectedResult = selectedResult;
            }
        }

        private bool _goFlag;
        private void Search()
        {
            if (_goFlag)
                return;
            _goFlag = true;

            SelectedResult = null;
            OnClearResults?.Invoke(this, EventArgs.Empty);
            SearchResults.Clear();
            GC.Collect();
            _imagesAsyncEnumerator = SelectedProvider.Provider.GetAsyncEnumerator(SearchText);
            NextImage();

            _goFlag = false;
        }

        private bool _nextFlag;
        private async void NextImage()
        {
            if (_imagesAsyncEnumerator is null)
                return;
            if (_nextFlag)
                return;
            _nextFlag = true;

            if (await _imagesAsyncEnumerator.MoveNextAsync())
            {
                SearchResults.Add(new ImageVM(SearchResults.Count + 1)
                {
                    Image = _imagesAsyncEnumerator.Current
                });
            }
                
            _nextFlag = false;
        }

        public bool TryGetImageData(out byte[] imageData)
        {
            if (SelectedResult is null)
            {
                MessageBox.Show("Selected is none.");
                imageData = null;
                return false;
            }

            using (var memoryStream = new MemoryStream())
            {
                BitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(SelectedResult.Image));
                encoder.Save(memoryStream);
                imageData = memoryStream.ToArray();
                return true;
            }
        }

    }
}
