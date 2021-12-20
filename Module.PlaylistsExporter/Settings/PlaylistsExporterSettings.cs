using System;
using System.Collections.Generic;
using MoreLinq;
using Newtonsoft.Json.Linq;
using Root.Abstractions;
using Root.Helpers;

namespace Module.PlaylistsExporter.Settings
{
    public class PlaylistsExporterSettings : BaseSettings, IPlaylistsExporterSettings
    {
        public string PlaylistsDirectoryPath { get; set; } = "";
        public string FilesLibraryPath { get; set; } = "";
        public string PlaylistsNewDirectoryName { get; set; } = "";
        public IReadOnlyCollection<string> PlaylistsForExport { get; set; } = Array.Empty<string>();

        public PlaylistsExporterSettings(string filePath) : base(filePath, true)
        {
        }

        protected override void PropertiesFromJObject(JToken rootObj)
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

        protected override JObject PropertiesToJObject()
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