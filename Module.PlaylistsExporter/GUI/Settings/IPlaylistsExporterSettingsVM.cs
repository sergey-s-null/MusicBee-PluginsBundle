using System.Windows.Input;
using Module.Settings.Gui.AbstractViewModels;

namespace Module.PlaylistsExporter.GUI.Settings;

public interface IPlaylistsExporterSettingsVM : IBaseSettingsVM
{
    string PlaylistsDirectoryPath { get; set; }
    string FilesLibraryPath { get; set; }
    string PlaylistsNewDirectoryName { get; set; }

    string PlaylistsBasePath { get; }
    IList<PlaylistVM> Playlists { get; }
    ICommand ApplyCheckStateToSelectedCmd { get; }
}