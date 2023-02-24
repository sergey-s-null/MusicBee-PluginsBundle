using System.Collections;
using System.Net;
using System.Reflection;

namespace Module.VkAudioDownloader.Helpers;

public static class CookieContainerEx
{
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