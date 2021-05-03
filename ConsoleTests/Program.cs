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
using System.Net;
using System.Diagnostics;
using System.Security.Cryptography;

namespace ConsoleTests
{
    class Program
    {
        private static string _tokenFilePath = @"../../delete_this/tm_token.txt";
        private static string _m3u8FilePath = Path.GetFullPath(@"../../delete_this/index.m3u8");
        private static string _baseUrlFilePath = Path.GetFullPath(@"../../delete_this/baseUrl.txt");
        private static string _m3u8ConvertedFilePath = Path.GetFullPath(@"../../delete_this/converted.m3u8");
        private static string _resultFilePath = Path.GetFullPath(@"../../delete_this/result.ts");

        static void Main(string[] args)
        {
            Part4();



            Console.WriteLine();
            Console.WriteLine("Press...");
            Console.ReadKey();
        }

        private static void Part1()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddAudioBypass();

            var api = new VkApi(serviceCollection);
            // TODOL delete auth data
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
            var audio = api.Audio.Get(new AudioGetParams() { Offset = 0, Count = 1 })[0];

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

        private static void Part2()
        {
            string baseUrl = File.ReadAllText(_baseUrlFilePath);

            string[] lines = File.ReadLines(_m3u8FilePath).ToArray();
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i][0] != '#')
                {
                    lines[i] = baseUrl + lines[i];
                }
            }

            File.WriteAllLines(_m3u8ConvertedFilePath, lines);
        }

        private static void Part3ffmpeg()
        {
            Process process = new Process();
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.FileName = "ffmpeg";
            process.StartInfo.Arguments = $"-protocol_whitelist \"file,http,https,tcp,tls,crypto\" " +
                $"-i \"{_m3u8ConvertedFilePath}\" " +
                $"-c copy \"{_resultFilePath}\"";

            process.Start();
            process.WaitForExit();
        }

        private static void Part4()
        {
            string[] lines = File.ReadAllLines(_m3u8ConvertedFilePath);
            Regex regex = new Regex("^#EXT-X-KEY:METHOD=(AES-128|NONE)(,URI=\"(.*)\")?");

            using (FileStream stream = File.OpenWrite(_resultFilePath))
            {
                string method = "";
                string keyUrl = "";
                foreach (string line in lines)
                {
                    if (line.Length == 0)
                        continue;
                    if (line[0] == '#')
                    {
                        Match match = regex.Match(line);
                        if (match.Success)
                        {
                            method = match.Groups[1].Value;
                            keyUrl = match.Groups[3].Value;
                        }
                    }
                    else
                    {
                        byte[] data;
                        using (WebClient webClient = new WebClient())
                        {
                            data = webClient.DownloadData(line);
                        }

                        if (method.Equals("NONE"))
                        {
                            stream.Write(data, 0, data.Length);
                        }
                        else if (method.Equals("AES-128"))
                        {
                            byte[] keyData;
                            using (WebClient webClient = new WebClient())
                            {
                                keyData = webClient.DownloadData(keyUrl);
                            }

                            Aes aes = Aes.Create();
                            aes.Key = keyData;
                            aes.IV = new byte[keyData.Length];
                            using (CryptoStream crStream = new CryptoStream(new MemoryStream(data), 
                                aes.CreateDecryptor(), CryptoStreamMode.Read))
                            {
                                crStream.CopyTo(stream);
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine("Warning! Unknown method!");
                        }
                    }
                }
            }
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
            // TODOL delete file
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
            // TODOL delete auth data
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
