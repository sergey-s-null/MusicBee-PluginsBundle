using System;
using System.Collections.Generic;
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
    public class SettingsDialogDTVM : ISettingsDialogVM
    {
        public bool IsLoaded => true;

        public IList<IModuleSettingsVM> Settings { get; }
        public IModuleSettingsVM SelectedSettingsModule { get; set; }
        public ICommand ResetCmd { get; }

        public SettingsDialogDTVM()
        {
            Settings = new List<IModuleSettingsVM>
            {
                new ModuleSettingsVM("Music downloader", new MusicDownloaderSettingsDTVM()),
                new ModuleSettingsVM("Artworks searcher", new ArtworksSearcherSettingsDTVM()),
                new ModuleSettingsVM("Playlists exporter", new PlaylistsExporterSettingsDTVM()),
            };
            SelectedSettingsModule = Settings.First();
            ResetCmd = new RelayCommand(_ => throw new NotSupportedException());
        }

        public void Load()
        {
            throw new NotSupportedException();
        }

        public bool Save()
        {
            throw new NotSupportedException();
        }

        public void Reset()
        {
            throw new NotSupportedException();
        }
    }
}