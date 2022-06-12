using Module.Vk.GUI.AbstractViewModels;
using Module.Vk.Settings;
using PropertyChanged;
using Root.GUI.ViewModels;

namespace Module.Vk.GUI.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class VkSettingsVM : BaseSettingsVM, IVkSettingsVM
    {
        public string AccessToken { get; set; } = "";

        private readonly IVkSettings _vkSettings;

        public VkSettingsVM(IVkSettings vkSettings)
            : base(vkSettings)
        {
            _vkSettings = vkSettings;
        }

        protected override void SetSettingsFromInnerServiceToViewModel()
        {
            AccessToken = _vkSettings.AccessToken;
        }

        protected override void SetSettingsFromViewModelToInnerService()
        {
            _vkSettings.AccessToken = AccessToken;
        }
    }
}