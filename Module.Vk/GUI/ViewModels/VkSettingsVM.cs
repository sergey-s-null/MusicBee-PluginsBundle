using Module.Vk.GUI.AbstractViewModels;
using Module.Vk.Settings;
using PropertyChanged;

namespace Module.Vk.GUI.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class VkSettingsVM : IVkSettingsVM
    {
        public string AccessToken { get; set; } = "";

        private readonly IVkSettings _vkSettings;

        public VkSettingsVM(IVkSettings vkSettings)
        {
            _vkSettings = vkSettings;
        }

        public void Load()
        {
            _vkSettings.Load();

            AccessToken = _vkSettings.AccessToken;
        }

        public void Save()
        {
            _vkSettings.AccessToken = AccessToken;

            _vkSettings.Save();
            // todo remove later
            // if (!_vkSettings.Save())
            // {
            //     MessageBox.Show(
            //         "Vk settings was not saved.",
            //         @"¯\_(ツ)_/¯",
            //         MessageBoxButton.OK
            //     );
            //     return false;
            // }
        }
    }
}