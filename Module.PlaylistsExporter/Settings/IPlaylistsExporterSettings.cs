using System.Collections.Generic;

namespace Module.PlaylistsExporter.Settings
{
    public interface IPlaylistsExporterSettings
    {
        string PlaylistsDirectoryPath { get; set; }
        string FilesLibraryPath { get; set; }
        string PlaylistsNewDirectoryName { get; set; }
        IReadOnlyCollection<string> PlaylistsForExport { get; set; }

        // todo
        void Load();
        void Save();
    }
}