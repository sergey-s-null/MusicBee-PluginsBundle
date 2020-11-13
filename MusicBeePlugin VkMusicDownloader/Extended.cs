using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace MusicBeePlugin_VkMusicDownloader
{
    public static class Extended
    {
        public static string ReadAllText(this WebResponse response)
        {
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }

        public static string Reverse(this string str)
        {
            char[] chars = str.ToArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        public static List<Cookie> GetAllCookies(this CookieContainer container)
        {
            var table = (Hashtable)typeof(CookieContainer).InvokeMember("m_domainTable", BindingFlags.NonPublic |
                BindingFlags.GetField | BindingFlags.Instance, null, container, new object[] { });

            List<Cookie> cookies = new List<Cookie>();
            foreach (string key in table.Keys)
            {
                var item = table[key];
                var items = (ICollection)item.GetType().GetProperty("Values").GetGetMethod().Invoke(item, null);
                foreach (CookieCollection cc in items)
                {
                    foreach (Cookie cookie in cc)
                    {
                        cookies.Add(cookie);
                    }
                }
            }
            return cookies;
        }

    }
}
