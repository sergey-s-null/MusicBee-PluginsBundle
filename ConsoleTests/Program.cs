using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
using Module.VkMusicDownloader.Helpers;
using VkNet;
using VkNet.AudioBypassService.Extensions;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace ConsoleTests
{
    class Program
    {
        private static string _tokenFilePath = @"../../delete_this/tm_token.txt";
        private static string _m3u8FilePath = Path.GetFullPath(@"../../delete_this/index.m3u8");
        private static string _baseUrlFilePath = Path.GetFullPath(@"../../delete_this/baseUrl.txt");
        private static string _m3u8ConvertedFilePath = Path.GetFullPath(@"../../delete_this/converted.m3u8");
        private static string _resultFilePath = Path.GetFullPath(@"../../delete_this/result.ts");
        private static string _encodedFilePath = Path.GetFullPath(@"../../delete_this/encoded.mp3");
        // from console
        private static string _compare1FilePath = Path.GetFullPath(@"../../delete_this/to_compare1.mp3");
        // from vk
        private static string _compare2FilePath = Path.GetFullPath(@"../../delete_this/to_compare2.mp3");

        static void Main(string[] args)
        {
            // var a = new Uri(@"D:\_BIG_FILES_\Music Library\");
            // var b = new Uri(@"D:\_BIG_FILES_\Music Library");
            
            var res1 = GetRelativePath(
                @"D:\_BIG_FILES_\Music Library\",
                @"D:\_BIG_FILES_\Music Library\Linkin Park\");

            var res2 = GetRelativePath(
                @"D:\_BIG_FILES_\Music Library",
                @"D:\_BIG_FILES_\Music Library\Linkin Park");
            
            Console.WriteLine(res1);
            Console.WriteLine(res2);
            
            // Console.WriteLine();
            // Console.WriteLine("Press...");
            // Console.ReadKey();
        }
        
        private static string GetRelativePath(string relativeTo, string path)
        {
            var relativeToUri = new Uri(relativeTo);
            var pathUri = new Uri(path);
            var relativeUri = relativeToUri.MakeRelativeUri(pathUri);
            
            return Uri
                .UnescapeDataString(relativeUri.ToString())
                .Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
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
                File.WriteAllBytes(_m3u8FilePath, data);
            }

            Regex regex = new Regex(@"(^.*/)index\.m3u8");
            Match match = regex.Match(audio.Url.AbsoluteUri);
            string baseUrl = match.Groups[1].Value;
            File.WriteAllText(_baseUrlFilePath, baseUrl);
        }

        private static bool TryLoadToken(out string token)
        {
            try
            {
                token = File.ReadAllText(_tokenFilePath);
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
                File.WriteAllText(_tokenFilePath, token);
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
                IVkApiEx.ConvertToMp3(audio.Url.AbsoluteUri, out string mp3Url);

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
