using Root.Abstractions;

namespace Module.Vk.GUI.AbstractViewModels
{
    public interface IVkSettingsVM : ISettings
    {
        string AccessToken { get; set; }
    }
}