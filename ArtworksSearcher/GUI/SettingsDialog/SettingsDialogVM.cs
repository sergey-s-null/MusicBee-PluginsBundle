// using Microsoft.WindowsAPICodePack.Dialogs;

using System.IO;
using System.Windows;

namespace ArtworksSearcher.GUI
{
    public class SettingsDialogVM : BaseViewModel
    {
        #region Bindings

        private string _googleCX;
        public string GoogleCX
        {
            get => _googleCX;
            set
            {
                _googleCX = value;
                NotifyPropChanged(nameof(GoogleCX));
            }
        }

        private string _googleKey;
        public string GoogleKey
        {
            get => _googleKey;
            set
            {
                _googleKey = value;
                NotifyPropChanged(nameof(GoogleKey));
            }
        }

        private string _parallelDownloadsCount;
        public string ParallelDownloadsCount
        {
            get => _parallelDownloadsCount;
            set
            {
                _parallelDownloadsCount = value;
                NotifyPropChanged(nameof(ParallelDownloadsCount));
            }
        }

        private string _osuSongsDir;
        public string OsuSongsDir
        {
            get => _osuSongsDir;
            set
            {
                _osuSongsDir = value;
                NotifyPropChanged(nameof(OsuSongsDir));
            }
        }

        private RelayCommand _changeOsuSongsDirCmd;
        public RelayCommand ChangeOsuSongsDirCmd
            => _changeOsuSongsDirCmd ?? (_changeOsuSongsDirCmd = new RelayCommand(_ => ChangeOsuSongsDir()));

        private string _minOsuImageByteSize;
        public string MinOsuImageByteSize
        {
            get => _minOsuImageByteSize;
            set
            {
                _minOsuImageByteSize = value;
                NotifyPropChanged(nameof(MinOsuImageByteSize));
            }
        }

        #endregion

        private Window _ownerWindow;
        private readonly Settings _settings;

        public SettingsDialogVM(Window ownerWindow, Settings settings)
        {
            _ownerWindow = ownerWindow;
            _settings = settings;

            _googleCX = _settings.GoogleCX;
            _googleKey = _settings.GoogleKey;
            _parallelDownloadsCount = _settings.ParallelDownloadsCount.ToString();
            _osuSongsDir = _settings.OsuSongsDir;
            _minOsuImageByteSize = _settings.MinOsuImageByteSize.ToString();
        }

        private void ChangeOsuSongsDir()
        {
            // TODO 
            // using (var dialog = new CommonOpenFileDialog())
            // {
            //     dialog.IsFolderPicker = true;
            //     dialog.DefaultDirectory = OsuSongsDir;
            //     if (dialog.ShowDialog(_ownerWindow) == CommonFileDialogResult.Ok)
            //         OsuSongsDir = dialog.FileName;
            // }
        }

        public bool SaveChanges()
        {
            if (!int.TryParse(ParallelDownloadsCount, out var parallelDownloadsCount))
            {
                MessageBox.Show("Wrong format of parallel downloads count.");
                return false;
            }
            if (parallelDownloadsCount < 1 || parallelDownloadsCount > Settings.MaxParallelDownloadsCount)
            {
                MessageBox.Show($"Parallel downloads count is out of range ({parallelDownloadsCount}).\n" +
                    $"Must be in range from 1 to {Settings.MaxParallelDownloadsCount}.");
                return false;
            }
            if (!Directory.Exists(OsuSongsDir))
            {
                MessageBox.Show("Specified osu songs directory does not exists.");
                return false;
            }
            if (!long.TryParse(MinOsuImageByteSize, out var minOsuImageByteSize))
            {
                MessageBox.Show("Wrong format of min osu image size.");
                return false;
            }
            if (minOsuImageByteSize < 0)
            {
                MessageBox.Show("Min osu image size must be >= 0.");
                return false;
            }

            _settings.GoogleCX = GoogleCX;
            _settings.GoogleKey = GoogleKey;
            _settings.ParallelDownloadsCount = parallelDownloadsCount;
            _settings.OsuSongsDir = OsuSongsDir;
            _settings.MinOsuImageByteSize = minOsuImageByteSize;

            if (!_settings.Save())
            {
                MessageBox.Show("Error save settings in file.");
                return false;
            }
            else
                return true;
        }


    }
}
