using System.Windows.Input;
using Module.Vk.Gui.AbstractViewModels;

namespace Module.Vk.Gui.DesignTimeViewModels;

public sealed class AuthorizationWindowDTVM : IAuthorizationWindowVM
{
    public event EventHandler? ClosingRequested;

    public string Login { get; set; } = "SomeLogin123";
    public string Password { get; set; } = "123passwordlkj";
    public string TwoFactorAuthCode { get; set; } = "5GU78";

    public bool AuthorizationInProgress => true;
    public ICommand AuthorizeCmd => null!;
    public bool CodeRequested => true;
    public ICommand Pass2FACodeCmd => null!;

    public bool AuthorizationResult => true;
}