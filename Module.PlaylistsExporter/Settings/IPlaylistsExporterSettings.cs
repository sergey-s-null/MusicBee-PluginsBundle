using Module.Settings.Entities.Abstract;

namespace Module.PlaylistsExporter.Settings;

public interface IPlaylistsExporterSettings : ISettings
{
    string PlaylistsDirectoryPath { get; set; }
    string FilesLibraryPath { get; set; }
    string PlaylistsNewDirectoryName { get; set; }
    IReadOnlyCollection<string> PlaylistsForExport { get; set; }
}