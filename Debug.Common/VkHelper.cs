using Microsoft.Extensions.DependencyInjection;
using VkNet;
using VkNet.Abstractions;
using VkNet.AudioBypassService.Extensions;
using VkNet.Model;

namespace Debug.Common;

public static class VkHelper
{
    private static readonly string TokenFilePath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "vk_secret.txt");

    public static IVkApi GetAuthorizedVkApi()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddAudioBypass();
        var vkApi = new VkApi(serviceCollection);
        AuthorizeWithStoredToken(vkApi);
        return vkApi;
    }

    private static void AuthorizeWithStoredToken(IVkApi vkApi)
    {
        var authParams = GetVkAuthParamsWithStoredToken();
        vkApi.Authorize(authParams);
        SaveToken(vkApi.Token);
    }

    private static ApiAuthParams GetVkAuthParamsWithStoredToken()
    {
        // todo delete secrets
        const string login = "";
        const string password = "";

        if (TryLoadToken(out var token))
        {
            Console.WriteLine("Token loaded.");
            return MakeAuthParams(login, password, token);
        }

        Console.WriteLine("Could not load token. Auth with default credentials.");
        return MakeAuthParams(login, password);
    }

    private static bool TryLoadToken(out string token)
    {
        try
        {
            LoadToken(out token);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Could not load token: {e}");
            token = "";
            return false;
        }
    }

    private static void LoadToken(out string token)
    {
        token = File.ReadAllText(TokenFilePath);
    }

    private static void SaveToken(string token)
    {
        File.WriteAllText(TokenFilePath, token);
    }

    private static ApiAuthParams MakeAuthParams(string login, string password, string? token = null)
    {
        var authParams = new ApiAuthParams
        {
            Login = login,
            Password = password,
            TwoFactorAuthorization = () =>
            {
                Console.Write("Code? ");
                return Console.ReadLine();
            }
        };

        if (token is not null)
        {
            authParams.AccessToken = token;
        }

        return authParams;
    }
}