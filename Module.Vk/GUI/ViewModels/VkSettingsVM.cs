using System.Windows;
using Module.Vk.GUI.AbstractViewModels;
using Module.Vk.Settings;
using PropertyChanged;

namespace Module.Vk.GUI.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class VkSettingsVM : IVkSettingsVM
    {
        public bool IsLoaded => _vkSettings.IsLoaded;

        public string AccessToken { get; set; } = "";

        private readonly IVkSettings _vkSettings;

        public VkSettingsVM(IVkSettings vkSettings)
        {
            _vkSettings = vkSettings;
        }

        public bool Load()
        {
            if (!_vkSettings.Load())
            {
                return false;
            }

            Reset();
            return true;
        }

        public bool Save()
        {
            _vkSettings.AccessToken = AccessToken;

            if (!_vkSettings.Save())
            {
                MessageBox.Show(
                    "Vk settings was not saved.",
                    @"¯\_(ツ)_/¯",
                    MessageBoxButton.OK
                );
                return false;
            }

            return true;
        }

        public void Reset()
        {
            AccessToken = _vkSettings.AccessToken;
        }
    }
}