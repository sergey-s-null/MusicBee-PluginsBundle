using Root.GUI.AbstractViewModels;

namespace Module.Vk.GUI.AbstractViewModels
{
    public interface IVkSettingsVM : IBaseSettingsVM
    {
        string AccessToken { get; set; }
    }
}