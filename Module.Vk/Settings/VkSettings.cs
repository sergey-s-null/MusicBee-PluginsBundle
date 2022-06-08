using Newtonsoft.Json.Linq;
using Root.Abstractions;
using Root.Helpers;
using Root.Services.Abstract;

namespace Module.Vk.Settings
{
    public class VkSettings : BaseSettings, IVkSettings
    {
        public string AccessToken { get; set; } = "";

        public VkSettings(IResourceManager resourceManager)
            : base(ResourcesHelper.VkSettingsPath, true, resourceManager)
        {
        }

        protected override void PropertiesFromJObject(JToken rootObj)
        {
            AccessToken = rootObj.Value<string>(nameof(AccessToken)) ?? "";
        }

        protected override JObject PropertiesToJObject()
        {
            return new JObject
            {
                [nameof(AccessToken)] = AccessToken
            };
        }
    }
}