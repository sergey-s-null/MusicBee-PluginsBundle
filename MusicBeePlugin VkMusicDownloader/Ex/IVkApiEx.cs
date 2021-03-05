﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using VkNet.Abstractions;
using VkNet.Model;

namespace VkMusicDownloader.Ex
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

        /// <param name="authDelegate">delegate to receive login with password</param>
        /// <param name="codeInputDelegate">delegate to receive two factor auth code</param>
        /// <returns>result of auth</returns>
        public static async Task<bool> TryAuthAsync(this IVkApi vkApi, 
            AuthDelegate authDelegate, CodeInputDelegate codeInputDelegate)
        {
            if (!authDelegate(out string login, out string password))
                return false;

            
            TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            try
            {
                await vkApi.AuthorizeAsync(new ApiAuthParams()
                {
                    Login = login,
                    Password = password,
                    TwoFactorAuthorization = () =>
                    {
                        TaskFactory taskFactory = new TaskFactory(scheduler);
                        Task<string> task = taskFactory.StartNew(() =>
                        {
                            if (codeInputDelegate(out string code))
                                return code;
                            else
                                return "";
                        });
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
        private static Regex _regex = new Regex(@"/[a-zA-Z\d]{6,}(/.*?[a-zA-Z\d]+?)/index.m3u8()");

        public static bool ConvertToMp3(string m3u8Url, out string mp3Url)
        {
            mp3Url = _regex.Replace(m3u8Url, @"$1$2.mp3");
            return mp3Url.IndexOf("m3u8", StringComparison.OrdinalIgnoreCase) == -1;
        }
    }
}
