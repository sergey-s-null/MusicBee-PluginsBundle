using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Module.ArtworksSearcher.GUI.Settings;
using Module.PlaylistsExporter.GUI.Settings;
using Module.Vk.GUI.AbstractViewModels;
using Module.VkAudioDownloader.GUI.AbstractViewModels;
using MusicBeePlugin.GUI.AbstractViewModels;
using PropertyChanged;

namespace MusicBeePlugin.GUI.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class SettingsDialogVM : ISettingsDialogVM
    {
        public IVkSettingsVM VkSettingsVM { get; }
        public IMusicDownloaderSettingsVM MusicDownloaderSettingsVM { get; }
        public IArtworksSearcherSettingsVM ArtworksSearcherSettingsVM { get; }
        public IPlaylistsExporterSettingsVM PlaylistsExporterSettingsVM { get; }

        public IList<IModuleSettingsVM> SettingsModules { get; } = new ObservableCollection<IModuleSettingsVM>();

        public IModuleSettingsVM SelectedSettingsModule { get; set; }
        
        public SettingsDialogVM(
            IVkSettingsVM vkSettingsVM,
            IMusicDownloaderSettingsVM musicDownloaderSettingsVM,
            IArtworksSearcherSettingsVM artworksSearcherSettingsVM,
            IPlaylistsExporterSettingsVM playlistsExporterSettingsVM)
        {
            VkSettingsVM = vkSettingsVM;
            MusicDownloaderSettingsVM = musicDownloaderSettingsVM;
            ArtworksSearcherSettingsVM = artworksSearcherSettingsVM;
            PlaylistsExporterSettingsVM = playlistsExporterSettingsVM;
            
            SettingsModules.Add(new ModuleSettingsVM("Vk",
                vkSettingsVM));
            SettingsModules.Add(new ModuleSettingsVM("Music downloader",
                musicDownloaderSettingsVM));
            SettingsModules.Add(new ModuleSettingsVM("Artworks searcher", 
                artworksSearcherSettingsVM));
            SettingsModules.Add(new ModuleSettingsVM("Playlists exporter", 
                playlistsExporterSettingsVM));

            SelectedSettingsModule = SettingsModules.First();
            
            Load();
        }

        // todo remake
        private bool Load()
        {
            return SettingsModules
                .All(setting => setting.ModuleSettings.Load());
        }

        public bool Save()
        {
            foreach (var setting in SettingsModules)
            {
                if (!setting.ModuleSettings.Save())
                {
                    return false;
                }
            }

            return true;
        }
    }
}