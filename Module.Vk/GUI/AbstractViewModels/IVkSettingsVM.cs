using Module.Settings.Gui.AbstractViewModels;

namespace Module.Vk.GUI.AbstractViewModels
{
    public interface IVkSettingsVM : IBaseSettingsVM
    {
        string AccessToken { get; set; }
        string UserId { get; set; }
    }
}