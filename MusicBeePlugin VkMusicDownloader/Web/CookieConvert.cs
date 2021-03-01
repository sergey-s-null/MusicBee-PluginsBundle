using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VkMusicDownloader.Web
{
    public static class CookieConvert
    {
        public static string Serialize(List<Cookie> cookies)
        {
            return JsonConvert.SerializeObject(cookies, Formatting.Indented);
        }

        public static List<Cookie> DeserializeCookies(string str)
        {
            JArray items = (JArray)JsonConvert.DeserializeObject(str);

            List<Cookie> result = new List<Cookie>();
            foreach (JObject obj in items)
            {
                result.Add(DeserializeCookie(obj));
            }

            return result;
        }

        private static Cookie DeserializeCookie(JObject obj)
        {
            Cookie c = new Cookie();

            c.Comment = obj.Value<string>("Comment");
            string uriStr = obj.Value<string>("CommentUri");
            if (uriStr is string)
                c.CommentUri = new Uri(uriStr);
            c.HttpOnly = obj.Value<bool>("HttpOnly");
            c.Discard = obj.Value<bool>("Discard");
            c.Domain = obj.Value<string>("Domain");
            c.Expired = obj.Value<bool>("Expired");
            c.Expires = obj.Value<DateTime>("Expires");
            string name = obj.Value<string>("Name");
            if (name is string && name.Length > 0)
                c.Name = name;
            c.Path = obj.Value<string>("Path");
            c.Port = obj.Value<string>("Port");
            c.Secure = obj.Value<bool>("Secure");
            c.Value = obj.Value<string>("Value");
            c.Version = obj.Value<int>("Version");

            return c;
        }
    }
}
