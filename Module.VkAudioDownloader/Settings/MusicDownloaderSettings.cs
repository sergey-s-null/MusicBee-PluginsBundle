using Newtonsoft.Json.Linq;
using Root.Services.Abstract;

namespace Module.VkAudioDownloader.Settings
{
    public class MusicDownloaderSettings : IMusicDownloaderSettings
    {
        // todo from config
        private const string AudioDownloaderSettingsPath = "AudioDownloader/settings.json";

        public string DownloadDirTemplate { get; set; } = "";
        public string FileNameTemplate { get; set; } = "";

        private readonly ISettingsJsonLoader _settingsJsonLoader;

        public MusicDownloaderSettings(ISettingsJsonLoader settingsJsonLoader)
        {
            _settingsJsonLoader = settingsJsonLoader;
        }

        public void Load()
        {
            // todo handle exceptions
            var jSettings = _settingsJsonLoader.Load(AudioDownloaderSettingsPath);
            SetSettingsFromJObject(jSettings);
        }

        public void Save()
        {
            // todo handle exceptions
            var jSettings = GetSettingsAsJObject();
            _settingsJsonLoader.Save(AudioDownloaderSettingsPath, jSettings);
        }

        private void SetSettingsFromJObject(JToken rootObj)
        {
            DownloadDirTemplate = rootObj.Value<string>(nameof(DownloadDirTemplate)) ?? "";
            FileNameTemplate = rootObj.Value<string>(nameof(FileNameTemplate)) ?? "";
        }

        private JObject GetSettingsAsJObject()
        {
            return new JObject
            {
                [nameof(DownloadDirTemplate)] = DownloadDirTemplate,
                [nameof(FileNameTemplate)] = FileNameTemplate
            };
        }
    }
}