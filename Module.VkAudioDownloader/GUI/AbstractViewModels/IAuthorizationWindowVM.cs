using System.Windows.Input;

namespace Module.VkAudioDownloader.GUI.AbstractViewModels;

public interface IAuthorizationWindowVM
{
    event EventHandler? ClosingRequested;

    string Login { get; set; }
    string Password { get; set; }
    string TwoFactorAuthCode { get; set; }

    bool AuthorizationInProgress { get; }
    ICommand AuthorizeCmd { get; }
    bool CodeRequested { get; }
    ICommand Pass2FACodeCmd { get; }

    bool AuthorizationResult { get; }
}