﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Module.ArtworksSearcher.Settings;

namespace Module.ArtworksSearcher.GUI.Settings
{
    // TODO проверку ввода с помощью интерфейса
    public class ArtworksSearcherSettingsVM : IArtworksSearcherSettingsVM, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public string GoogleCX { get; set; } = "";
        public string GoogleKey { get; set; } = "";
        public int MaxParallelDownloadsCount => _settings.MaxParallelDownloadsCount;
        public int ParallelDownloadsCount { get; set; } = 1;
        public string OsuSongsDir { get; set; } = "";
        public long MinOsuImageByteSize { get; set; }

        // TODO rename namespace
        private ICommand _changeOsuSongsDirCmd;
        public ICommand ChangeOsuSongsDirCmd =>
            _changeOsuSongsDirCmd ??= new Root.MVVM.RelayCommand(_ => ChangeOsuSongsDir());
        
        public bool IsLoaded => _settings.IsLoaded;

        private readonly IArtworksSearcherSettings _settings;

        public ArtworksSearcherSettingsVM(IArtworksSearcherSettings settings)
        {
            _settings = settings;
        }
        
        public void Load()
        {
            _settings.Load();
            
            Reset();
        }

        public bool Save()
        {
            _settings.GoogleCX = GoogleCX;
            _settings.GoogleKey = GoogleKey;
            _settings.ParallelDownloadsCount = ParallelDownloadsCount;
            _settings.OsuSongsDir = OsuSongsDir;
            _settings.MinOsuImageByteSize = MinOsuImageByteSize;
            
            if (_settings.Save()) return true;
            
            // TODO вероятно здесь не нужен диалог
            MessageBox.Show("Error save settings.");
            
            return false;
        }

        public void Reset()
        {
            GoogleCX = _settings.GoogleCX;
            GoogleKey = _settings.GoogleKey;
            ParallelDownloadsCount = _settings.ParallelDownloadsCount;
            OsuSongsDir = _settings.OsuSongsDir;
            MinOsuImageByteSize = _settings.MinOsuImageByteSize;
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