using System;
using Module.Vk.GUI.AbstractViewModels;

namespace Module.Vk.GUI.DesignTimeViewModels
{
    public class VkSettingsDTVM : IVkSettingsVM
    {
        public bool Loaded => false;

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