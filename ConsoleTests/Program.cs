using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicBeePlugin_VkMusicDownloader;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            //string cookiesFilename = "cookies.json";

            //VkAudioApi vkApi = new VkAudioApi(musicOwnerId, cookiesFilename);
            //bool res = vkApi.TryAuth(GetAuthData, InputCode);
            //Console.WriteLine($"auth res: {res}");
            //res = vkApi.TryGetAudioData(0, 20, out List<VkMusicData> list);

            //foreach (var data in list)
            //    Console.WriteLine(data.Title);
            
            Console.WriteLine();
            Console.WriteLine("Press...");
            Console.ReadKey();
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
