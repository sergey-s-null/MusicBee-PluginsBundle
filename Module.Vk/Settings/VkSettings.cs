using Module.Core.Helpers;
using Module.Settings.Entities.Abstract;
using Module.Settings.Exceptions;
using Module.Settings.Services.Abstract;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Module.Vk.Settings;

public sealed class VkSettings : BaseSettings, IVkSettings
{
    public string AccessToken { get; set; } = string.Empty;
    public long UserId { get; set; }

    public VkSettings(ISettingsJsonLoader settingsJsonLoader)
        : base(ResourcesHelper.VkSettingsPath, settingsJsonLoader)
    {
    }

    protected override void SetSettingsFromJObject(JObject jSettings)
    {
        try
        {
            AccessToken = jSettings.Value<string>(nameof(AccessToken)) ?? string.Empty;
            UserId = jSettings.Value<long>(nameof(UserId));
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
            [nameof(AccessToken)] = AccessToken,
            [nameof(UserId)] = UserId
        };
    }
}