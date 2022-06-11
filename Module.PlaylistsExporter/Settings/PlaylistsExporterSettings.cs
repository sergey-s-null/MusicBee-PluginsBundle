using System;
using System.Collections.Generic;
using MoreLinq;
using Newtonsoft.Json.Linq;
using Root.Helpers;
using Root.Services.Abstract;

namespace Module.PlaylistsExporter.Settings
{
    public class PlaylistsExporterSettings : IPlaylistsExporterSettings
    {
        // todo from config
        private const string PlaylistExporterSettingsPath = "PlaylistsExporter/settings.json";

        public string PlaylistsDirectoryPath { get; set; } = "";
        public string FilesLibraryPath { get; set; } = "";
        public string PlaylistsNewDirectoryName { get; set; } = "";
        public IReadOnlyCollection<string> PlaylistsForExport { get; set; } = Array.Empty<string>();

        private readonly ISettingsJsonLoader _settingsJsonLoader;

        public PlaylistsExporterSettings(ISettingsJsonLoader settingsJsonLoader)
        {
            _settingsJsonLoader = settingsJsonLoader;
        }

        public void Load()
        {
            // todo handle exceptions
            var jSettings = _settingsJsonLoader.Load(PlaylistExporterSettingsPath);
            SetSettingsFromJObject(jSettings);
        }

        public void Save()
        {
            // todo handle exceptions
            var jSettings = GetSettingsAsJObject();
            _settingsJsonLoader.Save(PlaylistExporterSettingsPath, jSettings);
        }

        private void SetSettingsFromJObject(JToken rootObj)
        {
            PlaylistsDirectoryPath = rootObj.Value<string>(nameof(PlaylistsDirectoryPath)) ?? "";
            FilesLibraryPath = rootObj.Value<string>(nameof(FilesLibraryPath)) ?? "";
            PlaylistsNewDirectoryName = rootObj.Value<string>(nameof(PlaylistsNewDirectoryName)) ?? "";
            PlaylistsForExport = rootObj.Value<JArray>(nameof(PlaylistsForExport))?
                                     .Values<string>()
                                     .WhereNotNull()
                                     .ToReadOnlyCollection()
                                 ?? Array.Empty<string>();
        }

        private JObject GetSettingsAsJObject()
        {
            var playlistsForExportJArray = new JArray();
            PlaylistsForExport.ForEach(x => playlistsForExportJArray.Add(x));

            return new JObject
            {
                [nameof(PlaylistsDirectoryPath)] = PlaylistsDirectoryPath,
                [nameof(FilesLibraryPath)] = FilesLibraryPath,
                [nameof(PlaylistsNewDirectoryName)] = PlaylistsNewDirectoryName,
                [nameof(PlaylistsForExport)] = playlistsForExportJArray,
            };
        }
    }
}