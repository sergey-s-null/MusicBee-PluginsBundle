using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VkNet.Abstractions;
using VkNet.Model;

namespace Module.VkAudioDownloader.Helpers
{
    public static class IVkApiEx
    {
        public delegate bool AuthDelegate(out string login, out string password);
        public delegate bool CodeInputDelegate(out string code);

        public static bool TryAuth(this IVkApi vkApi, string accessToken)
        {
            try
            {
                vkApi.Authorize(new ApiAuthParams()
                {
                    AccessToken = accessToken
                });
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public static bool TryAuth(this IVkApi vkApi, 
            AuthDelegate authDelegate, CodeInputDelegate codeInputDelegate)
        {
            if (!authDelegate(out var login, out var password))
                return false;
            
            try
            {
                vkApi.Authorize(new ApiAuthParams()
                {
                    Login = login,
                    Password = password,
                    TwoFactorAuthorization = () 
                        => codeInputDelegate(out var code) 
                            ? code 
                            : ""
                });
                
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        /// <param name="authDelegate">delegate to receive login with password</param>
        /// <param name="codeInputDelegate">delegate to receive two factor auth code</param>
        /// <returns>result of auth</returns>
        public static async Task<bool> TryAuthAsync(this IVkApi vkApi, 
            AuthDelegate authDelegate, CodeInputDelegate codeInputDelegate)
        {
            if (!authDelegate(out var login, out var password))
                return false;
            
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            try
            {
                await vkApi.AuthorizeAsync(new ApiAuthParams()
                {
                    Login = login,
                    Password = password,
                    TwoFactorAuthorization = () =>
                    {
                        var taskFactory = new TaskFactory(scheduler);
                        var task = taskFactory.StartNew(() 
                            => codeInputDelegate(out var code) 
                                ? code 
                                : "");
                        return task.Result;
                    }
                });
                return true;
            }
            catch
            {
                return false;
            }
        }

        // static without this
        private static Regex _regex = new Regex(@"/[0-9a-f]+(/audios)?/([0-9a-f]+)/index.m3u8");

        public static bool ConvertToMp3(string m3u8Url, out string mp3Url)
        {
            mp3Url = _regex.Replace(m3u8Url, @"$1/$2.mp3");
            return mp3Url.IndexOf("m3u8", StringComparison.OrdinalIgnoreCase) == -1;
        }
    }
}
