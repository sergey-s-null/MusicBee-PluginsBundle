using Root.Settings;

namespace Module.Vk.Settings
{
    public interface IVkSettings : ISettings
    {
        string AccessToken { get; set; }
    }
}