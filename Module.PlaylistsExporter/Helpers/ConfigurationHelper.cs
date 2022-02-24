using System.IO;
using Root.MusicBeeApi.Abstract;

namespace Module.PlaylistsExporter.Helpers
{
    internal static class ConfigurationHelper
    {
        public static string GetSettingsFilePath(IMusicBeeApi mbApi)
        {
            var dataPath = mbApi.Setting_GetPersistentStoragePath();
            
            return Path.Combine(dataPath, SettingsDirName, SettingsFileName);
        }
        
        private static string SettingsDirName => "Laiser399_PlaylistsExporter";

        private static string SettingsFileName => "settings.json";
    }
}