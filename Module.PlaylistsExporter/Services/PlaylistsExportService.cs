using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Module.PlaylistsExporter.Entities;
using Module.PlaylistsExporter.Settings;
using MoreLinq.Extensions;
using Root;
using Root.Helpers;

namespace Module.PlaylistsExporter.Services
{
    public class PlaylistsExportService : IPlaylistsExportService
    {
        private readonly IPlaylistsExporterSettings _settings;
        private readonly IPlaylistToLibraryConverter _converter;
        private readonly MusicBeeApiInterface _mbApi;
        
        public PlaylistsExportService(
            IPlaylistsExporterSettings settings,
            IPlaylistToLibraryConverter converter,
            MusicBeeApiInterface mbApi)
        {
            _settings = settings;
            _converter = converter;
            _mbApi = mbApi;
        }
        
        public void Export()
        {
            var converted = GetPlaylists()
                .Select(CollectPlaylistInfo)
                .Select(_converter.Convert)
                .ToReadOnlyCollection();

            // TODO удаление предыдущих, но с диалогом
            converted.ForEach(Save);
        }
        
        // TODO filter with settings
        private IReadOnlyCollection<string> GetPlaylists()
        {
            if (!_mbApi.Playlist_QueryPlaylistsEx(out var playlistPaths))
            {
                throw new Exception("Error on receiving playlists from MB Library");
            }

            return playlistPaths!;
        }
        
        private Playlist CollectPlaylistInfo(string p)
        {
            return new (SetM3UExtension(p), GetFilesForPlaylist(p));
        }
        
        private static string SetM3UExtension(string path)
        {
            return Path.ChangeExtension(path, "m3u");
        }
        
        private IReadOnlyCollection<string> GetFilesForPlaylist(string playlistPath)
        {
            if (!_mbApi.Playlist_QueryFilesEx(playlistPath, out var filePaths))
            {
                throw new Exception($"Error on receiving files for playlist \"{playlistPath}\" from MB Library");
            }

            return filePaths;
        }
        
        private static void Save(Playlist playlist)
        {
            var fileInfo = new FileInfo(playlist.Path);

            if (fileInfo.Directory is null)
            {
                throw new Exception("DirectoryInfo of playlist path in null");
            }
            
            if (!fileInfo.Directory.Exists)
            {
                fileInfo.Directory.Create();
            }

            File.WriteAllLines(playlist.Path, playlist.FilePaths);
        }
    }
}