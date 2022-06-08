using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Module.Vk.Helpers;
using Module.VkAudioDownloader.GUI.AbstractViewModels;
using Module.VkAudioDownloader.Settings;
using PropertyChanged;
using Root.MVVM;
using VkNet.Abstractions;

namespace Module.VkAudioDownloader.GUI.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class AuthorizationWindowVM : IAuthorizationWindowVM
    {
        public event EventHandler? ClosingRequested;

        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string TwoFactorAuthCode { get; set; } = string.Empty;

        public bool AuthorizationInProgress { get; private set; }
        public ICommand AuthorizeCmd => _authorizeCmd ??= new RelayCommand(_ => AuthorizeWrappedAsync());
        public bool CodeRequested { get; private set; }
        public ICommand Pass2FACodeCmd => _pass2FACodeCmd ??= new RelayCommand(_ => Pass2FACode());

        public bool AuthorizationResult { get; private set; }

        private ICommand? _authorizeCmd;
        private ICommand? _pass2FACodeCmd;

        private readonly IVkApi _vkApi;
        private readonly IMusicDownloaderSettings _musicDownloaderSettings;

        private readonly SemaphoreSlim _authorizationSemaphore = new(1, 1);
        private readonly SemaphoreSlim _pipelineSemaphore = new(1, 1);

        public AuthorizationWindowVM(
            IVkApi vkApi,
            IMusicDownloaderSettings musicDownloaderSettings)
        {
            _vkApi = vkApi;
            _musicDownloaderSettings = musicDownloaderSettings;
        }

        private async void AuthorizeWrappedAsync()
        {
            if (!await _authorizationSemaphore.WaitAsync(0))
            {
                return;
            }

            AuthorizationInProgress = true;

            AuthorizationResult = await AuthorizeAsync();

            AuthorizationInProgress = false;
            _authorizationSemaphore.Release();

            if (AuthorizationResult)
            {
                ClosingRequested?.Invoke(this, EventArgs.Empty);
            }
        }

        private async Task<bool> AuthorizeAsync()
        {
            if (!await _vkApi.TryAuthorizeWithValidationAsync(
                    Login, Password, () => Get2FACodeAsync().Result))
            {
                return false;
            }

            _musicDownloaderSettings.AccessToken = _vkApi.Token;
            _musicDownloaderSettings.Save();
            return true;
        }

        private void Pass2FACode()
        {
            _pipelineSemaphore.Release();
        }

        private async Task<string> Get2FACodeAsync()
        {
            if (!await _pipelineSemaphore.WaitAsync(0))
            {
                return string.Empty;
            }

            CodeRequested = true;

            if (!await _pipelineSemaphore.WaitAsync(60_000))
            {
                CodeRequested = false;
                return string.Empty;
            }

            CodeRequested = false;
            _pipelineSemaphore.Release();

            return TwoFactorAuthCode;
        }
    }
}