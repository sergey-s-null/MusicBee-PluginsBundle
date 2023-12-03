using Module.Settings.Gui.AbstractViewModels;

namespace Module.Vk.Gui.AbstractViewModels;

public interface IVkSettingsVM : IBaseSettingsVM
{
    string AccessToken { get; set; }
    string UserId { get; set; }
}