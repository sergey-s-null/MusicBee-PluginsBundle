using System.IO;
using Root.MusicBeeApi;

namespace Module.PlaylistsExporter.Helpers
{
    internal static class ConfigurationHelper
    {
        public static string GetSettingsFilePath(MusicBeeApiMemoryContainer mbApi)
        {
            var dataPath = mbApi.Setting_GetPersistentStoragePath();
            
            return Path.Combine(dataPath, SettingsDirName, SettingsFileName);
        }
        
        private static string SettingsDirName => "Laiser399_PlaylistsExporter";

        private static string SettingsFileName => "settings.json";
    }
}