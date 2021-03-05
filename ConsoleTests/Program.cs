using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using VkNet;
using VkNet.Abstractions;
using VkNet.AudioBypassService.Extensions;
using VkNet.Model;
using VkNet.Model.Attachments;
using VkNet.Utils;
using VkNet.Model.RequestParams;
using VkMusicDownloader.Ex;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            string tokenFilePath = @"C:\Users\Sergey\Desktop\vk_aga\token.txt";
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
            // TODOH delete auth data
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
