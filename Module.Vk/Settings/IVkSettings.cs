using Module.Settings.Entities.Abstract;

namespace Module.Vk.Settings;

public interface IVkSettings : ISettings
{
    string AccessToken { get; set; }
    long UserId { get; set; }
}