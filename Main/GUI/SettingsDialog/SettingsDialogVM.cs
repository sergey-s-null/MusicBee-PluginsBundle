using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Module.ArtworksSearcher.GUI.Settings;
using Module.VkMusicDownloader.GUI.Settings;
using Root.Abstractions;
using Root.MVVM;

namespace MusicBeePlugin.GUI.SettingsDialog
{
    public class SettingsDialogVM : ISettings, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsLoaded => Settings.All(s => s.ModuleSettings.IsLoaded);
        
        public ObservableCollection<ModuleSettingsVM> Settings { get; } = new ();

        public ModuleSettingsVM SelectedSettingsModule { get; set; }
        
        private RelayCommand _resetCmd;
        public RelayCommand ResetCmd
            => _resetCmd ??= new RelayCommand(_ => Reset());
        
        public SettingsDialogVM(
            IMusicDownloaderSettingsVM musicDownloaderSettingsVM,
            IArtworksSearcherSettingsVM artworksSearcherSettingsVM)
        {
            Settings.Add(new ModuleSettingsVM("Music downloader", 
                musicDownloaderSettingsVM));
            Settings.Add(new ModuleSettingsVM("Artworks searcher", 
                artworksSearcherSettingsVM));

            SelectFirstSettingsModule();
            
            Load();
        }

        private void SelectFirstSettingsModule()
        {
            if (Settings.Count > 0)
            {
                SelectedSettingsModule = Settings.First();
            }
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