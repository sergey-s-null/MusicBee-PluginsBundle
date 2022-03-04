using System.Collections.Generic;
using System.Windows.Input;
using Root.Abstractions;

namespace Module.PlaylistsExporter.GUI.Settings
{
    public interface IPlaylistsExporterSettingsVM : ISettings
    {
        string PlaylistsDirectoryPath { get; set; }
        string FilesLibraryPath { get; set; }
        string PlaylistsNewDirectoryName { get; set; }

        string PlaylistsBasePath { get; }
        IList<PlaylistVM> Playlists { get; }
        ICommand ApplyCheckStateToSelectedCmd { get; }
    }
}