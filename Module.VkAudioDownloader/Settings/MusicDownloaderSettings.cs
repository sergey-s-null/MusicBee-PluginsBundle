using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Root.Exceptions;
using Root.Services.Abstract;
using Root.Settings;

namespace Module.VkAudioDownloader.Settings
{
    public class MusicDownloaderSettings : BaseSettings, IMusicDownloaderSettings
    {
        // todo from config
        private const string AudioDownloaderSettingsPath = "AudioDownloader/settings.json";

        public string DownloadDirTemplate { get; set; } = "";
        public string FileNameTemplate { get; set; } = "";

        public MusicDownloaderSettings(ISettingsJsonLoader settingsJsonLoader)
            : base(AudioDownloaderSettingsPath, settingsJsonLoader)
        {
        }

        protected override void SetSettingsFromJObject(JObject rootObj)
        {
            try
            {
                DownloadDirTemplate = rootObj.Value<string>(nameof(DownloadDirTemplate)) ?? "";
                FileNameTemplate = rootObj.Value<string>(nameof(FileNameTemplate)) ?? "";
            }
            catch (JsonException e)
            {
                throw new SettingsLoadException("Error on set settings from json object.", e);
            }
        }

        protected override JObject GetSettingsAsJObject()
        {
            return new JObject
            {
                [nameof(DownloadDirTemplate)] = DownloadDirTemplate,
                [nameof(FileNameTemplate)] = FileNameTemplate
            };
        }
    }
}