using System.Windows;
using System.Windows.Input;
using Module.ArtworksSearcher.Settings;
using PropertyChanged;
using Root.MVVM;

namespace Module.ArtworksSearcher.GUI.Settings
{
    [AddINotifyPropertyChangedInterface]
    public class ArtworksSearcherSettingsVM : IArtworksSearcherSettingsVM
    {
        public string GoogleCX { get; set; } = "";
        public string GoogleKey { get; set; } = "";
        public int ParallelDownloadsCount { get; set; } = 1;
        public string OsuSongsDir { get; set; } = "";
        public long MinOsuImageByteSize { get; set; }

        private ICommand? _changeOsuSongsDirCmd;

        public ICommand ChangeOsuSongsDirCmd =>
            _changeOsuSongsDirCmd ??= new RelayCommand(_ => ChangeOsuSongsDir());

        private readonly IArtworksSearcherSettings _artworksSearcherSettings;

        public ArtworksSearcherSettingsVM(IArtworksSearcherSettings artworksSearcherSettings)
        {
            _artworksSearcherSettings = artworksSearcherSettings;
        }

        public bool Load()
        {
            if (!_artworksSearcherSettings.Load())
            {
                return false;
            }

            Reset();
            return true;
        }

        public bool Save()
        {
            _artworksSearcherSettings.GoogleCX = GoogleCX;
            _artworksSearcherSettings.GoogleKey = GoogleKey;
            _artworksSearcherSettings.ParallelDownloadsCount = ParallelDownloadsCount;
            _artworksSearcherSettings.OsuSongsDir = OsuSongsDir;
            _artworksSearcherSettings.MinOsuImageByteSize = MinOsuImageByteSize;

            if (_artworksSearcherSettings.Save()) return true;

            // TODO вероятно здесь не нужен диалог
            MessageBox.Show("Error save settings.");

            return false;
        }

        public void Reset()
        {
            GoogleCX = _artworksSearcherSettings.GoogleCX;
            GoogleKey = _artworksSearcherSettings.GoogleKey;
            ParallelDownloadsCount = _artworksSearcherSettings.ParallelDownloadsCount;
            OsuSongsDir = _artworksSearcherSettings.OsuSongsDir;
            MinOsuImageByteSize = _artworksSearcherSettings.MinOsuImageByteSize;
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
    }
}