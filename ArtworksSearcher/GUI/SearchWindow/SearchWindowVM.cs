using ArtworksSearcher.ImagesProviders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ArtworksSearcher.GUI
{
    public class SearchWindowVM : BaseViewModel
    {

        #region Bindings

        private string _artist = "";
        public string Artist
        {
            get => _artist;
            set
            {
                _artist = value;
                ResetSearchText();
                NotifyPropChanged(nameof(Artist));
            }
        }

        private string _title = "";
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                ResetSearchText();
                NotifyPropChanged(nameof(Title));
            }
        }

        private string _searchText = "";
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                NotifyPropChanged(nameof(SearchText));
            }
        }

        private RelayCommand _resetSearchTextCmd;
        public RelayCommand ResetSearchTextCmd
            => _resetSearchTextCmd ?? (_resetSearchTextCmd = new RelayCommand(_ => ResetSearchText()));

        private RelayCommand _selectImageCmd;
        public RelayCommand SelectImageCmd
            => _selectImageCmd ?? (_selectImageCmd = new RelayCommand(arg => SelectImage(arg)));

        private ObservableCollection<ImageVM> _searchResults;
        public ObservableCollection<ImageVM> SearchResults =>
            _searchResults ?? (_searchResults = new ObservableCollection<ImageVM>());

        private ImageVM _selectedResult = null;
        public ImageVM SelectedResult
        {
            get => _selectedResult;
            set
            {
                _selectedResult = value;
                NotifyPropChanged(nameof(SelectedResult));
            }
        }

        private ImagesProviderVM[] _imagesProviders;
        public ImagesProviderVM[] ImagesProviders
        {
            get
            {
                if (_imagesProviders is null)
                {
                    _imagesProviders = new ImagesProviderVM[]
                    {
                        new ImagesProviderVM()
                        {
                            // TODO from settings
                            //Plugin.Settings.GoogleCX, Plugin.Settings.GoogleKey
                            Provider = new GoogleImagesProvider("", ""),
                            Name = "Google"
                        },
                        new ImagesProviderVM()
                        {
                            // TODO from settings
                            // Plugin.Settings.OsuSongsDir
                            // MinSize = Plugin.Settings.MinOsuImageByteSize
                            Provider = new OsuImagesProvider("C:\\Games\\osu!\\Songs") { MinSize = 4_000 },
                            Name = "Osu!dir"
                        }
                    };
                }
                return _imagesProviders;
            }
        }

        private ImagesProviderVM _selectedProvider;
        public ImagesProviderVM SelectedProvider
        {
            get => _selectedProvider ?? (_selectedProvider = ImagesProviders[0]);
            set
            {
                _selectedProvider = value;
                NotifyPropChanged(nameof(SelectedProvider));
            }
        }

        private RelayCommand _searchCmd;
        public RelayCommand SearchCmd
            => _searchCmd ?? (_searchCmd = new RelayCommand(_ => Search()));

        private RelayCommand _nextImageCmd;
        public RelayCommand NextImageCmd
            => _nextImageCmd ?? (_nextImageCmd = new RelayCommand(_ => NextImage()));

        #endregion

        private IAsyncEnumerator<BitmapImage> _imagesAsyncEnumerator;
        public event EventHandler OnClearResults;

        public SearchWindowVM()
        {
            ResetSearchText();
        }

        private void ResetSearchText()
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

        private bool _goFlag = false;
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

        private bool _nextFlag = false;
        private async void NextImage()
        {
            if (_imagesAsyncEnumerator is null)
                return;
            if (_nextFlag)
                return;
            _nextFlag = true;

            if (await _imagesAsyncEnumerator.MoveNext())
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

            using (MemoryStream memoryStream = new MemoryStream())
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
