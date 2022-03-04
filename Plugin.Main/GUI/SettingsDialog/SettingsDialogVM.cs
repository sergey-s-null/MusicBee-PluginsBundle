using System.Collections.Generic;
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
        public bool IsLoaded => SettingsModules.All(s => s.ModuleSettings.IsLoaded);

        public IMusicDownloaderSettingsVM MusicDownloaderSettingsVM { get; }
        public IArtworksSearcherSettingsVM ArtworksSearcherSettingsVM { get; }
        public IPlaylistsExporterSettingsVM PlaylistsExporterSettingsVM { get; }

        public IList<IModuleSettingsVM> SettingsModules { get; } = new ObservableCollection<IModuleSettingsVM>();

        public IModuleSettingsVM SelectedSettingsModule { get; set; }
        
        private RelayCommand? _resetCmd;
        public ICommand ResetCmd
            => _resetCmd ??= new RelayCommand(_ => Reset());
        
        public SettingsDialogVM(
            IMusicDownloaderSettingsVM musicDownloaderSettingsVM,
            IArtworksSearcherSettingsVM artworksSearcherSettingsVM,
            IPlaylistsExporterSettingsVM playlistsExporterSettingsVM)
        {
            MusicDownloaderSettingsVM = musicDownloaderSettingsVM;
            ArtworksSearcherSettingsVM = artworksSearcherSettingsVM;
            PlaylistsExporterSettingsVM = playlistsExporterSettingsVM;
            
            SettingsModules.Add(new ModuleSettingsVM("Music downloader",
                musicDownloaderSettingsVM));
            SettingsModules.Add(new ModuleSettingsVM("Artworks searcher", 
                artworksSearcherSettingsVM));
            SettingsModules.Add(new ModuleSettingsVM("Playlists exporter", 
                playlistsExporterSettingsVM));

            SelectedSettingsModule = SettingsModules.First();
            
            Load();
        }

        public void Load()
        {
            foreach (var setting in SettingsModules)
            {
                setting.ModuleSettings.Load();
            }
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

        public void Reset()
        {
            foreach (var setting in SettingsModules)
            {
                setting.ModuleSettings.Reset();
            }
        }
    }
}