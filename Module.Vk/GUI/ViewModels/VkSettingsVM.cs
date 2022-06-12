using Module.Vk.GUI.AbstractViewModels;
using Module.Vk.Settings;
using PropertyChanged;
using Root.Exceptions;

namespace Module.Vk.GUI.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class VkSettingsVM : IVkSettingsVM
    {
        public bool Loaded { get; private set; }

        public string AccessToken { get; set; } = "";

        private readonly IVkSettings _vkSettings;

        public VkSettingsVM(IVkSettings vkSettings)
        {
            _vkSettings = vkSettings;
        }

        public void Load()
        {
            try
            {
                _vkSettings.Load();
                Loaded = true;
            }
            catch (SettingsLoadException)
            {
                // todo display error
                Loaded = false;
            }

            AccessToken = _vkSettings.AccessToken;
        }

        public void Save()
        {
            _vkSettings.AccessToken = AccessToken;

            _vkSettings.Save();
        }
    }
}