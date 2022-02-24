using System.IO;
using Root.MusicBeeApi.Abstract;

namespace Module.VkAudioDownloader.Helpers
{
    public static class ConfigurationHelper
    {
        public static string GetSettingsFilePath(IMusicBeeApi mbApi)
        {
            var dataPath = mbApi.Setting_GetPersistentStoragePath();
            
            return Path.Combine(dataPath, SettingsDirName, SettingsFileName);
        }
        
        public static string GetSettingsDirPath(IMusicBeeApi mbApi)
        {
            var dataPath = mbApi.Setting_GetPersistentStoragePath();
            
            return Path.Combine(dataPath, SettingsDirName);
        }

        public static string SettingsDirName => "Laiser399_VkAudioDownloader";

        public static string SettingsFileName => "settings.json";
    }
}