using MusicBeePlugin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MusicBeePlugin_VkMusicDownloader
{
    public class WebSession
    {
        private CookieContainer _cookies = new CookieContainer();
        public string UserAgent = "";

        public WebResponse Get(string url, bool allowAutoRedirect = false)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.UserAgent = UserAgent;
            request.CookieContainer = _cookies;
            request.AllowAutoRedirect = allowAutoRedirect;

            WebResponse response = request.GetResponse();
            _cookies = request.CookieContainer;
            return response;
        }

        public async Task<WebResponse> GetAsync(string url, bool allowAutoRedirect = false)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.UserAgent = UserAgent;
            request.CookieContainer = _cookies;
            request.AllowAutoRedirect = allowAutoRedirect;

            WebResponse response = await request.GetResponseAsync();
            _cookies = request.CookieContainer;
            return response;
        }

        public WebResponse Post(string url, Dictionary<string, string> body = null, bool? allowAutoRedirect = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.UserAgent = UserAgent;
            request.CookieContainer = _cookies;
            if (allowAutoRedirect is object)
                request.AllowAutoRedirect = (bool)allowAutoRedirect;

            if (body is object)
                SetBody(request, body);

            WebResponse response = request.GetResponse();
            _cookies = request.CookieContainer;
            return response;
        }

        public async Task<WebResponse> PostAsync(string url, Dictionary<string, string> body = null,
            bool? allowAutoRedirect = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.UserAgent = UserAgent;
            request.CookieContainer = _cookies;
            if (allowAutoRedirect is object)
                request.AllowAutoRedirect = (bool)allowAutoRedirect;

            if (body is object)
                SetBody(request, body);

            WebResponse response = await request.GetResponseAsync();
            _cookies = request.CookieContainer;
            return response;
        }

        public void DownloadFile(string url, string filePath)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.UserAgent = UserAgent;
            request.CookieContainer = _cookies;

            WebResponse response = request.GetResponse();
            _cookies = request.CookieContainer;

            using (FileStream outStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                response.GetResponseStream().CopyTo(outStream);
            }
        }

        public async Task DownloadFileAsync(string url, string filePath)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.UserAgent = UserAgent;
            request.CookieContainer = _cookies;

            WebResponse response = await request.GetResponseAsync();
            //WebResponse response = request.GetResponse();
            _cookies = request.CookieContainer;

            using (FileStream outStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                await response.GetResponseStream().CopyToAsync(outStream);
            }
        }

        public void AddCookie(Cookie cookie)
        {
            _cookies.Add(cookie);
        }

        public List<Cookie> GetCookies()
        {
            return _cookies.GetAllCookies();
        }

        private static void SetBody(HttpWebRequest request, Dictionary<string, string> body)
        {
            request.ContentType = "application/x-www-form-urlencoded";
            using (var memStream = new MemoryStream())
            using (var writer = new StreamWriter(memStream))
            {
                bool isFirst = true;
                foreach (var pair in body)
                {
                    if (isFirst)
                        isFirst = false;
                    else
                        writer.Write('&');
                    writer.Write(pair.Key);
                    writer.Write('=');
                    writer.Write(pair.Value);
                }
                writer.Flush();

                byte[] bytes = memStream.ToArray();
                request.ContentLength = bytes.Length;
                request.GetRequestStream().Write(bytes, 0, bytes.Length);
            }
        }
    }
}
