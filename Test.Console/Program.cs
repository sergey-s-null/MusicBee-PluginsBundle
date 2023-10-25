using System.Net;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
using VkNet;
using VkNet.Abstractions;
using VkNet.AudioBypassService.Extensions;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace Test.Console;

internal static class Program
{
    private static readonly string TokenFilePath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "vk_secret.txt");

    private static readonly string M3U8FilePath = Path.GetFullPath(@"../../delete_this/index.m3u8");
    private static readonly string BaseUrlFilePath = Path.GetFullPath(@"../../delete_this/baseUrl.txt");

    static async Task Main(string[] args)
    {
    }

    // download m3u8 and baseUrl
    private static void Part1()
    {
        var api = GetAuthorizedVkApi();

        // TODO change indices
        var audio = api.Audio.Get(new AudioGetParams() { Offset = 1, Count = 1 })[0];

        using (var webClient = new WebClient())
        {
            var data = webClient.DownloadData(audio.Url);
            File.WriteAllBytes(M3U8FilePath, data);
        }

        var regex = new Regex(@"(^.*/)index\.m3u8");
        var match = regex.Match(audio.Url.AbsoluteUri);
        var baseUrl = match.Groups[1].Value;
        File.WriteAllText(BaseUrlFilePath, baseUrl);
    }

    private static IVkApi GetAuthorizedVkApi()
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
            System.Console.WriteLine("Token loaded.");
            return MakeAuthParams(login, password, token);
        }

        System.Console.WriteLine("Could not load token. Auth with default credentials.");
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
            System.Console.WriteLine($"Could not load token: {e}");
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

    private static ApiAuthParams MakeAuthParams(string login, string password)
    {
        return new ApiAuthParams
        {
            Login = login,
            Password = password,
            TwoFactorAuthorization = () =>
            {
                System.Console.Write("Code? ");
                return System.Console.ReadLine();
            }
        };
    }

    private static ApiAuthParams MakeAuthParams(string login, string password, string token)
    {
        return new ApiAuthParams
        {
            Login = login,
            Password = password,
            TwoFactorAuthorization = () =>
            {
                System.Console.Write("Code? ");
                return System.Console.ReadLine();
            },
            AccessToken = token
        };
    }
}