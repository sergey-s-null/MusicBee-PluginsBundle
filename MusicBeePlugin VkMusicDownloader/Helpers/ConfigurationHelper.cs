using System.Configuration;
using System.IO;
using System.Reflection;
using MusicBeePlugin;

namespace VkMusicDownloader.Ex
{
    public static class ConfigurationHelper
    {
        public static string GetSettingsFilePath(Plugin.MusicBeeApiInterface mbApi)
        {
            var dataPath = mbApi.Setting_GetPersistentStoragePath();
            
            return Path.Combine(dataPath, SettingsDirName, SettingsFileName);
        }
        
        public static string GetSettingsDirPath(Plugin.MusicBeeApiInterface mbApi)
        {
            var dataPath = mbApi.Setting_GetPersistentStoragePath();
            
            return Path.Combine(dataPath, SettingsDirName);
        }

        public static string SettingsDirName => "Laiser399_VkAudioDownloader";

        public static string SettingsFileName => "settings.json";
    }
}