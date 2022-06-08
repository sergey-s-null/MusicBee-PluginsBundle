using Module.Vk.GUI.AbstractViewModels;

namespace Module.Vk.GUI.DesignTimeViewModels
{
    public class VkSettingsDTVM : IVkSettingsVM
    {
        public string AccessToken { get; set; } = "{private access token}";
    }
}