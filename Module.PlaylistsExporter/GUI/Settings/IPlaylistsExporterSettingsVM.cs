using System.Collections.ObjectModel;
using Root.Abstractions;
using Root.MVVM;

namespace Module.PlaylistsExporter.GUI.Settings
{
    public interface IPlaylistsExporterSettingsVM : ISettings
    {
        string PlaylistsDirectoryPath { get; set; }
        string FilesLibraryPath { get; set; }
        string PlaylistsNewDirectoryName { get; set; }
    
        string PlaylistsBasePath { get; }
        ObservableCollection<PlaylistVM> Playlists { get; }
        RelayCommand ApplyCheckStateToSelectedCmd { get; }
    }
}