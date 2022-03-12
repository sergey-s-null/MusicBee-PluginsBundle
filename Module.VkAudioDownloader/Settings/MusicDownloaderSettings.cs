using Newtonsoft.Json.Linq;
using Root.Abstractions;
using Root.Helpers;
using Root.Services.Abstract;

namespace Module.VkAudioDownloader.Settings
{
    public class MusicDownloaderSettings : BaseSettings, IMusicDownloaderSettings
    {
        public string DownloadDirTemplate { get; set; } = "";
        public string FileNameTemplate { get; set; } = "";
        public string AccessToken { get; set; } = "";
        
        public MusicDownloaderSettings(IResourceManager resourceManager) 
            : base(ResourcesHelper.AudioDownloaderSettingsPath, true, resourceManager)
        {
            
        }
        
        protected override void PropertiesFromJObject(JToken rootObj)
        {
            DownloadDirTemplate = rootObj.Value<string>(nameof(DownloadDirTemplate)) ?? "";
            FileNameTemplate = rootObj.Value<string>(nameof(FileNameTemplate)) ?? "";
            AccessToken = rootObj.Value<string>(nameof(AccessToken)) ?? "";
        }

        protected override JObject PropertiesToJObject()
        {
            return new JObject
            {
                [nameof(DownloadDirTemplate)] = DownloadDirTemplate,
                [nameof(FileNameTemplate)] = FileNameTemplate,
                [nameof(AccessToken)] = AccessToken
            };
        }
    }
}
