﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Module.ArtworksSearcher.GUI.Settings;
using Module.PlaylistsExporter.GUI.Settings;
using Module.VkAudioDownloader.GUI.Settings;
using PropertyChanged;
using Root.MVVM;

namespace MusicBeePlugin.GUI.SettingsDialog
{
    [AddINotifyPropertyChangedInterface]
    public class SettingsDialogVM : ISettingsDialogVM
    {
        public bool IsLoaded => Settings.All(s => s.ModuleSettings.IsLoaded);
        
        public IList<IModuleSettingsVM> Settings { get; } = new ObservableCollection<IModuleSettingsVM>();

        public IModuleSettingsVM SelectedSettingsModule { get; set; }
        
        private RelayCommand? _resetCmd;
        public ICommand ResetCmd
            => _resetCmd ??= new RelayCommand(_ => Reset());
        
        public SettingsDialogVM(
            IMusicDownloaderSettingsVM musicDownloaderSettingsVM,
            IArtworksSearcherSettingsVM artworksSearcherSettingsVM,
            IPlaylistsExporterSettingsVM playlistsExporterSettingsVM)
        {
            Settings.Add(new ModuleSettingsVM("Music downloader", 
                musicDownloaderSettingsVM));
            Settings.Add(new ModuleSettingsVM("Artworks searcher", 
                artworksSearcherSettingsVM));
            Settings.Add(new ModuleSettingsVM("Playlists exporter", 
                playlistsExporterSettingsVM));

            SelectedSettingsModule = Settings.First();
            
            Load();
        }

        public void Load()
        {
            foreach (var setting in Settings)
            {
                setting.ModuleSettings.Load();
            }
        }

        public bool Save()
        {
            foreach (var setting in Settings)
            {
                if (!setting.ModuleSettings.Save())
                {
                    return false;
                }
            }

            return true;
        }

        public void Reset()
        {
            foreach (var setting in Settings)
            {
                setting.ModuleSettings.Reset();
            }
        }
    }
}