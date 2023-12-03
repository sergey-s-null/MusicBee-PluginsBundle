using System.Windows;
using Module.Vk.Gui.Services.Abstract;
using Module.Vk.Gui.Views;
using Module.Vk.Logic.Exceptions;
using Module.Vk.Logic.Helpers;
using Module.Vk.Logic.Services.Abstract;
using VkNet.Abstractions;

namespace Module.Vk.Gui.Services;

public sealed class VkApiAuthorizationsService : IVkApiAuthorizationsService
{
    private readonly IVkApi _vkApi;
    private readonly IVkSettings _vkSettings;
    private readonly Func<AuthorizationWindow> _authorizationWindowFactory;

    public VkApiAuthorizationsService(
        IVkApi vkApi,
        IVkSettings vkSettings,
        Func<AuthorizationWindow> authorizationWindowFactory)
    {
        _vkApi = vkApi;
        _vkSettings = vkSettings;
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
            _vkApi.AuthorizeWithValidation(_vkSettings.AccessToken);
            return true;
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

        var authorizationWindow = _authorizationWindowFactory();
        var authorized = authorizationWindow.ShowDialog();
        return authorized;
    }

    private static bool ContinueWithAuthorizationDialog(Exception e)
    {
        var dialogResult = MessageBox.Show(
            "Got error on authorization with access token from settings.\n\n" +
            $"{e}\n\n" +
            "Continue with dialog authorization?",
            "?",
            MessageBoxButton.YesNo
        );

        return dialogResult == MessageBoxResult.Yes;
    }
}