using System;
using System.Windows.Input;
using Module.Vk.GUI.AbstractViewModels;
using Root.MVVM;

namespace Module.Vk.GUI.DesignTimeViewModels
{
    public class VkSettingsDTVM : IVkSettingsVM
    {
        public bool Loaded => false;

        public ICommand ReloadCmd { get; } = RelayCommand.Empty;

        public string AccessToken { get; set; } = "{private access token}";

        public void Load()
        {
            throw new NotSupportedException();
        }

        public void Save()
        {
            throw new NotSupportedException();
        }
    }
}