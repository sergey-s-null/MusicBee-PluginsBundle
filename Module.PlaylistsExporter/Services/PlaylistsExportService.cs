using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Module.MusicBee.Abstract;
using Module.MusicBee.Extension.Helpers;
using Module.PlaylistsExporter.Entities;
using Module.PlaylistsExporter.Settings;
using MoreLinq.Extensions;
using Root.Helpers;

namespace Module.PlaylistsExporter.Services
{
    public sealed class PlaylistsExportService : IPlaylistsExportService
    {
        private readonly IPlaylistsExporterSettings _settings;
        private readonly IPlaylistToLibraryConverter _converter;
        private readonly IMusicBeeApi _mbApi;

        public PlaylistsExportService(
            IPlaylistsExporterSettings settings,
            IPlaylistToLibraryConverter converter,
            IMusicBeeApi mbApi)
        {
            _settings = settings;
            _converter = converter;
            _mbApi = mbApi;
        }

        public void CleanAndExport()
        {
            GetExistingExportedPlaylists()
                .ForEach(File.Delete);

            Directory.GetDirectories(GetExportDirectoryPath())
                .ForEach(Directory.Delete);

            GetPlaylistsForExport()
                .Select(CollectPlaylistInfo)
                .Select(_converter.Convert)
                .ToReadOnlyCollection()
                .ForEach(Save);
        }

        public IReadOnlyCollection<string> GetExistingExportedPlaylists()
        {
            var exportDirectoryPath = GetExportDirectoryPath();

            return DirectoryHelper.GetFilesRecursively(exportDirectoryPath);
        }

        private string GetExportDirectoryPath()
        {
            return Path.Combine(_settings.FilesLibraryPath, _settings.PlaylistsNewDirectoryName);
        }

        private IReadOnlyCollection<string> GetPlaylistsForExport()
        {
            if (!_mbApi.Playlist_QueryPlaylistsEx(out var playlistPaths))
            {
                throw new Exception("Error on receiving playlists from MB Library");
            }

            return playlistPaths!
                .Intersect(_settings.PlaylistsForExport)
                .ToReadOnlyCollection();
        }

        private Playlist CollectPlaylistInfo(string p)
        {
            return new Playlist(SetM3UExtension(p), GetFilesForPlaylist(p));
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