using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using MusicBeePlugin.Annotations;
using Root.Abstractions;
using VkMusicDownloader.GUI.Settings;

namespace MusicBeePlugin.GUI.SettingsDialog
{
    public class SettingsDialogVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ModuleSettingsVM> Settings { get; } = new ();

        public ModuleSettingsVM SelectedSettingsModule { get; set; }
        
        public SettingsDialogVM(IMusicDownloaderSettingsVM musicDownloaderSettingsVM)
        {
            Settings.Add(new ModuleSettingsVM("Music downloader", 
                musicDownloaderSettingsVM));
            
            musicDownloaderSettingsVM.Load();// TODO сделать это более универсализированным. Мб через ISettings
        }
    }
}