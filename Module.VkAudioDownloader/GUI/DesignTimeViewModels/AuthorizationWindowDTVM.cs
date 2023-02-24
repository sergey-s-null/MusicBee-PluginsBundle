using System.Windows.Input;
using Module.Mvvm.Extension;
using Module.VkAudioDownloader.GUI.AbstractViewModels;

// Unused variable warning
#pragma warning disable CS0067

namespace Module.VkAudioDownloader.GUI.DesignTimeViewModels;

public sealed class AuthorizationWindowDTVM : IAuthorizationWindowVM
{
    public event EventHandler? ClosingRequested;

    public string Login { get; set; } = "SomeLogin123";
    public string Password { get; set; } = "123passwordlkj";
    public string TwoFactorAuthCode { get; set; } = "5GU78";

    public bool AuthorizationInProgress => true;
    public ICommand AuthorizeCmd { get; } = new RelayCommand(_ => { });
    public bool CodeRequested => true;
    public ICommand Pass2FACodeCmd { get; } = new RelayCommand(_ => { });

    public bool AuthorizationResult => true;
}