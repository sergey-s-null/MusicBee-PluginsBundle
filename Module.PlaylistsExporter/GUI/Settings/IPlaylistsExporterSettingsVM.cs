using System.Collections.Generic;
using System.Windows.Input;
using Root.GUI.AbstractViewModels;

namespace Module.PlaylistsExporter.GUI.Settings
{
    public interface IPlaylistsExporterSettingsVM : IBaseSettingsVM
    {
        string PlaylistsDirectoryPath { get; set; }
        string FilesLibraryPath { get; set; }
        string PlaylistsNewDirectoryName { get; set; }

        string PlaylistsBasePath { get; }
        IList<PlaylistVM> Playlists { get; }
        ICommand ApplyCheckStateToSelectedCmd { get; }
    }
}