using System;
using System.Windows;
using CoreModule.Vk.Exceptions;
using CoreModule.Vk.Helpers;
using Module.VkAudioDownloader.GUI.Factories;
using Module.VkAudioDownloader.Settings;
using Root.Services.Abstract;
using VkNet.Abstractions;

namespace MusicBeePlugin.Services
{
    public class VkApiAuthorizationsService : IVkApiAuthorizationsService
    {
        private readonly IVkApi _vkApi;
        private readonly IMusicDownloaderSettings _musicDownloaderSettings;
        private readonly IAuthorizationWindowFactory _authorizationWindowFactory;

        public VkApiAuthorizationsService(
            IVkApi vkApi,
            IMusicDownloaderSettings musicDownloaderSettings,
            IAuthorizationWindowFactory authorizationWindowFactory)
        {
            _vkApi = vkApi;
            _musicDownloaderSettings = musicDownloaderSettings;
            _authorizationWindowFactory = authorizationWindowFactory;
        }

        public bool AuthorizeVkApiIfNeeded()
        {
            if (_vkApi.IsAuthorizedWithCheck())
            {
                return true;
            }

            try
            {
                _vkApi.AuthorizeWithValidation(_musicDownloaderSettings.AccessToken);
            }
            catch (ArgumentException e)
            {
                if (!ContinueWithAuthorizationDialog(e))
                {
                    return false;
                }
            }
            catch (VkApiAuthorizationException e)
            {
                if (!ContinueWithAuthorizationDialog(e))
                {
                    return false;
                }
            }
            
            var authorizationWindow = _authorizationWindowFactory.Create();
            var authorized = authorizationWindow.ShowDialog();
            return authorized;
        }

        private static bool ContinueWithAuthorizationDialog(Exception e)
        {
            var dialogResult = MessageBox.Show(
                "Got error on authorization with access token from settings.\n" +
                $"Error message: \"{e.Message}\".\n\n" +
                "Continue with dialog authorization?",
                "?",
                MessageBoxButton.YesNo
            );

            return dialogResult == MessageBoxResult.Yes;
        }
    }
}