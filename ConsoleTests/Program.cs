using System.Net;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
using Module.Vk.Helpers;
using Module.VkAudioDownloader.Helpers;
using VkNet;
using VkNet.AudioBypassService.Extensions;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace ConsoleTests
{
    class Program
    {
        private const string TokenFilePath = @"../../delete_this/tm_token.txt";
        private static readonly string M3U8FilePath = Path.GetFullPath(@"../../delete_this/index.m3u8");
        private static readonly string BaseUrlFilePath = Path.GetFullPath(@"../../delete_this/baseUrl.txt");

        static void Main(string[] args)
        {
        }

        // download m3u8 and baseUrl
        private static void Part1()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddAudioBypass();

            var api = new VkApi(serviceCollection);
            // TODO delete auth data
            string login = "";
            string password = "";


            ApiAuthParams authParams;
            if (TryLoadToken(out string token))
            {
                authParams = MakeAuthParams(login, password, token);
            }
            else
            {
                authParams = MakeAuthParams(login, password);
            }

            api.Authorize(authParams);
            TrySaveToken(api.Token);

            // TODO change indices
            var audio = api.Audio.Get(new AudioGetParams() { Offset = 1, Count = 1 })[0];

            using (WebClient webClient = new WebClient())
            {
                byte[] data = webClient.DownloadData(audio.Url);
                File.WriteAllBytes(M3U8FilePath, data);
            }

            Regex regex = new Regex(@"(^.*/)index\.m3u8");
            Match match = regex.Match(audio.Url.AbsoluteUri);
            string baseUrl = match.Groups[1].Value;
            File.WriteAllText(BaseUrlFilePath, baseUrl);
        }

        private static bool TryLoadToken(out string token)
        {
            try
            {
                token = File.ReadAllText(TokenFilePath);
                return true;
            }
            catch
            {
                token = "";
                return false;
            }
        }

        private static bool TrySaveToken(string token)
        {
            try
            {
                File.WriteAllText(TokenFilePath, token);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void TestVkNet()
        {
            // TODO delete file
            string tokenFilePath = @"tm_token.txt";
            string token;
            try
            {
                token = File.ReadAllText(tokenFilePath);
            }
            catch
            {
                token = "";
            }

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddAudioBypass();

            var api = new VkApi(serviceCollection);
            // TODO delete auth data
            string login = "";
            string password = "";

            ApiAuthParams authParams;
            if (token.Length == 0)
                authParams = MakeAuthParams(login, password);
            else
                authParams = MakeAuthParams(login, password, token);
            api.Authorize(authParams);

            int count = 15;
            foreach (var audio in api.Audio.GetIter())
            {
                VkApiHelper.ConvertToMp3(audio.Url.AbsoluteUri, out string mp3Url);

                Console.WriteLine(audio.Artist);
                Console.WriteLine(audio.Title);
                Console.WriteLine(audio.Url.AbsoluteUri);
                Console.WriteLine(mp3Url);
                Console.WriteLine();
                if (--count == 0)
                    break;
            }

            File.WriteAllText(tokenFilePath, api.Token);

            Console.WriteLine();
            Console.WriteLine("Press...");
            Console.ReadKey();
        }

        private static ApiAuthParams MakeAuthParams(string login, string password)
        {
            return new ApiAuthParams()
            {
                Login = login,
                Password = password,
                TwoFactorAuthorization = () =>
                {
                    Console.Write("Code? ");
                    return Console.ReadLine();
                }
            };
        }

        private static ApiAuthParams MakeAuthParams(string login, string password, string token)
        {
            return new ApiAuthParams()
            {
                Login = login,
                Password = password,
                TwoFactorAuthorization = () =>
                {
                    Console.Write("Code? ");
                    return Console.ReadLine();
                },
                AccessToken = token
            };
        }

        private static bool GetAuthData(out string login, out string password)
        {
            login = "";
            password = "";
            return true;
        }

        private static bool InputCode(out string code)
        {
            Console.Write("Enter code? ");
            code = Console.ReadLine();
            return true;
        }
    }
}