using Module.Settings.Entities.Abstract;

namespace Module.Vk.Logic.Services.Abstract;

public interface IVkSettings : ISettings
{
    string AccessToken { get; set; }
    long UserId { get; set; }
}