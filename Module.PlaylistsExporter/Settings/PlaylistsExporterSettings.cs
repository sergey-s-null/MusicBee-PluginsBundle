using Newtonsoft.Json.Linq;
using Root.Abstractions;

namespace Module.PlaylistsExporter.Settings
{
    public class PlaylistsExporterSettings : BaseSettings, IPlaylistsExporterSettings
    {
        public string PlaylistsDirectoryPath { get; set; } = "";
        public string FilesLibraryPath { get; set; } = "";
        public string PlaylistsNewDirectoryName { get; set; } = "";
        
        public PlaylistsExporterSettings(string filePath) : base(filePath, true)
        {
        }

        protected override void PropertiesFromJObject(JToken rootObj)
        {
            PlaylistsDirectoryPath = rootObj.Value<string>(nameof(PlaylistsDirectoryPath)) ?? "";
            FilesLibraryPath = rootObj.Value<string>(nameof(FilesLibraryPath)) ?? "";
            PlaylistsNewDirectoryName = rootObj.Value<string>(nameof(PlaylistsNewDirectoryName)) ?? "";
        }

        protected override JObject PropertiesToJObject()
        {
            return new()
            {
                [nameof(PlaylistsDirectoryPath)] = PlaylistsDirectoryPath,
                [nameof(FilesLibraryPath)] = FilesLibraryPath,
                [nameof(PlaylistsNewDirectoryName)] = PlaylistsNewDirectoryName,
            };
        }

    }
}