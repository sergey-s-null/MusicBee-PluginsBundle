using Root.Abstractions;

namespace Module.PlaylistsExporter.Settings
{
    public interface IPlaylistsExporterSettings : ISettings
    {
        string PlaylistsDirectoryPath { get; set; }
        string FilesLibraryPath { get; set; }
        string PlaylistsNewDirectoryName { get; set; }
    }
}