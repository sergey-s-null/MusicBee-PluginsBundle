using System;
using Module.Vk.GUI.AbstractViewModels;

namespace Module.Vk.GUI.DesignTimeViewModels
{
    public class VkSettingsDTVM : IVkSettingsVM
    {
        public bool IsLoaded => true;

        public string AccessToken { get; set; } = "{private access token}";

        public bool Load()
        {
            throw new NotSupportedException();
        }

        public bool Save()
        {
            throw new NotSupportedException();
        }

        public void Reset()
        {
            throw new NotSupportedException();
        }
    }
}