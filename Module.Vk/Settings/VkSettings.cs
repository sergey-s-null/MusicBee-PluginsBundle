using Newtonsoft.Json.Linq;
using Root.Services.Abstract;

namespace Module.Vk.Settings
{
    public class VkSettings : IVkSettings
    {
        // todo move to configuration (like App.config)
        private const string VkSettingsPath = "Vk/settings.json";

        public string AccessToken { get; set; } = "";

        private readonly ISettingsJsonLoader _settingsJsonLoader;

        public VkSettings(ISettingsJsonLoader settingsJsonLoader)
        {
            _settingsJsonLoader = settingsJsonLoader;
        }

        public void Load()
        {
            // todo handle exceptions
            var jSettings = _settingsJsonLoader.Load(VkSettingsPath);
            SetSettingsFromJObject(jSettings);
        }

        public void Save()
        {
            // todo handle exceptions
            var jSettings = GetSettingsAsJObject();
            _settingsJsonLoader.Save(VkSettingsPath, jSettings);
        }

        private void SetSettingsFromJObject(JToken rootObj)
        {
            AccessToken = rootObj.Value<string>(nameof(AccessToken)) ?? "";
        }

        private JObject GetSettingsAsJObject()
        {
            return new JObject
            {
                [nameof(AccessToken)] = AccessToken
            };
        }
    }
}