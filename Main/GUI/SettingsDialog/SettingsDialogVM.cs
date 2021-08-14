using System.Collections.ObjectModel;
using System.ComponentModel;
using Module.ArtworksSearcher.GUI.Settings;
using Module.VkMusicDownloader.GUI.Settings;

namespace MusicBeePlugin.GUI.SettingsDialog
{
    public class SettingsDialogVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ModuleSettingsVM> Settings { get; } = new ();

        public ModuleSettingsVM SelectedSettingsModule { get; set; }
        
        public SettingsDialogVM(IMusicDownloaderSettingsVM musicDownloaderSettingsVM,
            IArtworksSearcherSettingsVM artworksSearcherSettingsVM)
        {
            Settings.Add(new ModuleSettingsVM("Music downloader", 
                musicDownloaderSettingsVM));
            Settings.Add(new ModuleSettingsVM("Artworks searcher", 
                artworksSearcherSettingsVM));
            
            musicDownloaderSettingsVM.Load();// TODO сделать это более универсализированным. Мб через ISettings
            artworksSearcherSettingsVM.Load();
        }
    }
}