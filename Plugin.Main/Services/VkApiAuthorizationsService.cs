using Module.VkAudioDownloader.GUI.Factories;
using Module.VkAudioDownloader.Helpers;
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
            if (_vkApi.IsAuthorized)
            {
                return true;
            }

            if (_vkApi.TryAuthorizeWithValidation(_musicDownloaderSettings.AccessToken))
            {
                return true;
            }

            var authorizationWindow = _authorizationWindowFactory.Create();
            var authorized = authorizationWindow.ShowDialog();
            return authorized;
        }
    }
}