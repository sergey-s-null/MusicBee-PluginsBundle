using System.Collections.Generic;
using System.IO;
using System.Linq;
using Module.PlaylistsExporter.Entities;
using Module.PlaylistsExporter.Settings;
using Root.Helpers;

namespace Module.PlaylistsExporter.Services
{
    public class PlaylistToLibraryConverter : IPlaylistToLibraryConverter
    {
        private readonly IPlaylistsExporterSettings _settings;
        
        public PlaylistToLibraryConverter(IPlaylistsExporterSettings settings)
        {
            _settings = settings;
        }
        
        public Playlist Convert(Playlist playlist)
        {
            var convertedPlaylistPath = ConvertPlaylistPath(playlist.Path);

            var convertedFilePaths = ConvertFilePaths(
                convertedPlaylistPath, playlist.FilePaths);

            return new Playlist(convertedPlaylistPath, convertedFilePaths);
        }

        private string ConvertPlaylistPath(string playlistPath)
        {
            var playlistRelativePath = PathHelper.GetRelativeToDirectoryPath(
                _settings.PlaylistsDirectoryPath, playlistPath);
            
            return Path.Combine(_settings.FilesLibraryPath,
                _settings.PlaylistsNewDirectoryName, playlistRelativePath);
        }

        private static IReadOnlyCollection<string> ConvertFilePaths(string convertedPlaylistPath, 
            IEnumerable<string> filePaths)
        {
            return filePaths
                .Select(fp => PathHelper.GetRelativePath(convertedPlaylistPath, fp))
                .ToReadOnlyCollection();
        }
    }
}