using System.IO;
using Root;

namespace VkMusicDownloader.Helpers
{
    public static class ConfigurationHelper
    {
        public static string GetSettingsFilePath(MusicBeeApiInterface mbApi)
        {
            var dataPath = mbApi.Setting_GetPersistentStoragePath();
            
            return Path.Combine(dataPath, SettingsDirName, SettingsFileName);
        }
        
        public static string GetSettingsDirPath(MusicBeeApiInterface mbApi)
        {
            var dataPath = mbApi.Setting_GetPersistentStoragePath();
            
            return Path.Combine(dataPath, SettingsDirName);
        }

        public static string SettingsDirName => "Laiser399_VkAudioDownloader";

        public static string SettingsFileName => "settings.json";
    }
}