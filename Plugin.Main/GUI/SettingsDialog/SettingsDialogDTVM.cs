using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Module.ArtworksSearcher.GUI.Settings;
using Module.PlaylistsExporter.GUI.Settings;
using Module.Vk.GUI.AbstractViewModels;
using Module.Vk.GUI.DesignTimeViewModels;
using Module.VkAudioDownloader.GUI.AbstractViewModels;
using Module.VkAudioDownloader.GUI.DesignTimeViewModels;
using PropertyChanged;
using Root.MVVM;

namespace MusicBeePlugin.GUI.SettingsDialog
{
    [AddINotifyPropertyChangedInterface]
    public class SettingsDialogDTVM : ISettingsDialogVM
    {
        public bool IsLoaded => true;

        public IVkSettingsVM VkSettingsVM { get; }
        public IMusicDownloaderSettingsVM MusicDownloaderSettingsVM { get; }
        public IArtworksSearcherSettingsVM ArtworksSearcherSettingsVM { get; }
        public IPlaylistsExporterSettingsVM PlaylistsExporterSettingsVM { get; }
        
        public IList<IModuleSettingsVM> SettingsModules { get; }
        public IModuleSettingsVM SelectedSettingsModule { get; set; }
        public ICommand ResetCmd { get; }

        public SettingsDialogDTVM()
        {
            VkSettingsVM = new VkSettingsDTVM();
            MusicDownloaderSettingsVM = new MusicDownloaderSettingsDTVM();
            ArtworksSearcherSettingsVM = new ArtworksSearcherSettingsDTVM();
            PlaylistsExporterSettingsVM = new PlaylistsExporterSettingsDTVM();
            
            SettingsModules = new List<IModuleSettingsVM>
            {
                new ModuleSettingsVM("Music downloader", MusicDownloaderSettingsVM),
                new ModuleSettingsVM("Artworks searcher", ArtworksSearcherSettingsVM),
                new ModuleSettingsVM("Playlists exporter", PlaylistsExporterSettingsVM),
            };
            SelectedSettingsModule = SettingsModules.First();
            ResetCmd = new RelayCommand(_ => throw new NotSupportedException());
        }

        public bool Load()
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