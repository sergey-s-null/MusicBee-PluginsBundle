using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Root.Exceptions;
using Root.Services.Abstract;
using Root.Settings;

namespace Module.Vk.Settings
{
    public class VkSettings : BaseSettings, IVkSettings
    {
        // todo move to configuration (like App.config)
        private const string VkSettingsPath = "Vk/settings.json";

        public string AccessToken { get; set; } = "";

        public VkSettings(ISettingsJsonLoader settingsJsonLoader)
            : base(VkSettingsPath, settingsJsonLoader)
        {
        }

        protected override void SetSettingsFromJObject(JObject jSettings)
        {
            try
            {
                AccessToken = jSettings.Value<string>(nameof(AccessToken)) ?? "";
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
                [nameof(AccessToken)] = AccessToken
            };
        }
    }
}