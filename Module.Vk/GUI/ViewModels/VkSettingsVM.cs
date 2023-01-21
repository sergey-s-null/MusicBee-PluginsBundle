using System;
using System.Windows;
using Module.Vk.GUI.AbstractViewModels;
using Module.Vk.Settings;
using PropertyChanged;
using Root.GUI.ViewModels;

namespace Module.Vk.GUI.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public sealed class VkSettingsVM : BaseSettingsVM, IVkSettingsVM
    {
        public string AccessToken { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;

        private readonly IVkSettings _vkSettings;

        public VkSettingsVM(IVkSettings vkSettings)
            : base(vkSettings)
        {
            _vkSettings = vkSettings;
        }

        protected override void SetSettingsFromInnerServiceToViewModel()
        {
            AccessToken = _vkSettings.AccessToken;
            UserId = _vkSettings.UserId.ToString();
        }

        protected override void SetSettingsFromViewModelToInnerService()
        {
            if (!long.TryParse(UserId, out var userId))
            {
                MessageBox.Show(
                    "Could not parse user id.\n" +
                    $"Actual value is \"{UserId}\".",
                    "Error!",
                    MessageBoxButton.OK
                );
                throw new ArgumentException(@"Could not parse UserId.", nameof(UserId));
            }

            _vkSettings.AccessToken = AccessToken;
            _vkSettings.UserId = userId;
        }
    }
}