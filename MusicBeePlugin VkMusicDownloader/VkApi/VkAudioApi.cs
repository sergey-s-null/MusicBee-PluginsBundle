using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading;
using System.Web;
using System.IO;
using MusicBeePlugin;
using VkMusicDownloader.Ex;
using VkMusicDownloader.Web;

namespace VkMusicDownloader.VkApi
{
    public delegate bool AuthDelegate(out string login, out string password);
    public delegate bool CodeInputDelegate(out string code);


    public class VkAudioApi
    {
        private string _ownerId;
        private string _cookiesFilePath;
        private WebSession _session = new WebSession();
        private bool _isFirstMusicDataReceived = false;
        private List<FirstVkMusicData> _firstMusicData = new List<FirstVkMusicData>();
        private Dictionary<string, VkMusicData> _musicDataById = new Dictionary<string, VkMusicData>();

        private int _msBetweenRequests = 1500;
        private long _lastRequestTime = 0;
        
        public bool IsAuthorized => HasSid(_session.GetCookies());

        public VkAudioApi(string ownerId, string cookiesFilePath)
        {
            _ownerId = ownerId;
            _cookiesFilePath = cookiesFilePath;
            _session.UserAgent = _userAgent;
            LoadCookies();
        }

        private void LoadCookies()
        {
            if (!File.Exists(_cookiesFilePath))
                return;

            string content = File.ReadAllText(_cookiesFilePath);
            List<Cookie> cookies = CookieConvert.DeserializeCookies(content);
            foreach (Cookie cookie in cookies)
                _session.AddCookie(cookie);
        }

        private void SaveCookies()
        {
            List<Cookie> cookies = _session.GetCookies();
            string content = CookieConvert.Serialize(cookies);
            File.WriteAllText(_cookiesFilePath, content);
        }

        public bool TryAuth(AuthDelegate authDataInput, CodeInputDelegate codeInput)
        {
            try
            {
                return Auth(authDataInput, codeInput);
            }
            catch
            {
                return false;
            }
        }

        private bool Auth(AuthDelegate authDataInput, CodeInputDelegate codeInput)
        {
            WebResponse response = _session.Get("https://vk.com/");

            if (HasSid(_session.GetCookies()))
                return true;

            string text = response.ReadAllText();

            Regex regex = new Regex("name=\"lg_h\" value=\"([a-z0-9]+)\"");
            Match match = regex.Match(text);
            if (!match.Success)
                return false;

            if (!authDataInput(out string login, out string password))
                return false;

            response = _session.Post("https://login.vk.com/", new Dictionary<string, string>
            {
                ["act"] = "login",
                ["role"] = "al_frame",
                ["_origin"] = "https://vk.com",
                ["utf8"] = "1",
                ["email"] = login,
                ["pass"] = password,
                ["lg_h"] = match.Groups[1].Value,
            });
            text = response.ReadAllText();

            if (!text.Contains("act=authcheck"))
                return false;

            response = _session.Get("https://vk.com/login?act=authcheck");
            text = response.ReadAllText();

            regex = new Regex("\\{.*?act: 'a_authcheck_code'.+?hash: '([a-z_0-9]+)'.*?\\}");
            match = regex.Match(text);
            if (!match.Success)
                return false;

            if (!codeInput(out string code))
                return false;

            response = _session.Post("https://vk.com/al_login.php", new Dictionary<string, string>
            {
                ["act"] = "a_authcheck_code",
                ["al"] = "1",
                ["code"] = code,
                ["remember"] = "0",
                ["hash"] = match.Groups[1].Value
            });
            text = response.ReadAllText();

            JObject data = (JObject)JsonConvert.DeserializeObject(text.Substring(4));
            string status = data.Value<JArray>("payload")[0].ToObject<string>();

            if (status == "4")
            {
                string path = JsonConvert.DeserializeObject<string>(data["payload"][1][0].ToObject<string>());
                _session.Get("https://vk.com" + path);
                SaveCookies();
                return true;
            }
            else
                return false;
        }
        
        public void ClearFirstMusicData()
        {
            _isFirstMusicDataReceived = false;
            _firstMusicData.Clear();
        }

        /// <summary>
        /// TOO MANY EXCEPTIONS
        /// </summary>
        /// <param name="shift"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<List<VkMusicData>> GetAudioDataAsync(int shift, int count)
        {
            await ReceiveFirstAudioDataAsync();

            shift = Math.Max(0, shift);
            shift = Math.Min(_firstMusicData.Count, shift);
            count = Math.Min(_firstMusicData.Count - shift, count);

            
            for (int i = shift; i < shift + count; ++i)
            {
                var toReceive = _firstMusicData.Skip(shift).Take(count);
                await ReceiveVkAudioDataAsync(toReceive);
            }

            List<VkMusicData> result = new List<VkMusicData>();
            for (int i = shift; i < shift + count; ++i)
                result.Add(_musicDataById[_firstMusicData[i].Id]);
            return result;
        }

        private async Task ReceiveFirstAudioDataAsync()
        {
            if (_isFirstMusicDataReceived)
                return;

            // default cookies
            foreach (var cookie in _defaultCookies)
                _session.AddCookie(cookie);

            int count = _session.GetCookies().Count;
            
            WebResponse response = await _session.PostAsync("https://m.vk.com/audio", new Dictionary<string, string>
            {
                ["act"] = "load_section",
                ["owner_id"] = _ownerId,
                ["playlist_id"] = "-1",
                ["offset"] = "0",
                ["type"] = "playlist",
                ["is_loading_all"] = "1"
            });
            string text = response.ReadAllText();
            JArray data = (JArray)JsonConvert.DeserializeObject<JToken>(text)["data"][0]["list"];

            // prepare data to next data request
            foreach (var track in data)
            {
                string[] hashes = track[13].ToObject<string>().Split('/');
                _firstMusicData.Add(new FirstVkMusicData
                {
                    OwnerId = track[1].ToString(),
                    Id = track[0].ToString(),
                    Hash1 = hashes[2],
                    Hash2 = hashes[5],
                });
            }

            _isFirstMusicDataReceived = true;
        }

        private async Task ReceiveVkAudioDataAsync(IEnumerable<FirstVkMusicData> firstMusicData, bool update = false)
        {
            int counter = 0;
            StringBuilder builder = new StringBuilder();
            bool isFirst = true;
            foreach (var musicData in firstMusicData)
            {
                if (_musicDataById.ContainsKey(musicData.Id) && !update)
                    continue;

                if (isFirst)
                    isFirst = false;
                else
                    builder.Append(',');
                builder.Append(musicData.OwnerId);
                builder.Append('_');
                builder.Append(musicData.Id);
                builder.Append('_');
                builder.Append(musicData.Hash1);
                builder.Append('_');
                builder.Append(musicData.Hash2);
                counter = (counter + 1) % 10;

                if (counter == 0)
                {
                    List<VkMusicData> dataList = await ReceiveVkAudioDataAsync(builder.ToString());
                    AddToCollection(dataList);

                    isFirst = true;
                    builder.Clear();
                }
            }

            if (counter > 0)
            {
                List<VkMusicData> dataList = await ReceiveVkAudioDataAsync(builder.ToString());
                AddToCollection(dataList);
            }
        }

        private async Task<List<VkMusicData>> ReceiveVkAudioDataAsync(string idsStringRepr)
        {
            await Task.Run(WaitBeforeNextRequest);
            
            WebResponse response = await _session.PostAsync("https://m.vk.com/audio", new Dictionary<string, string>
            {
                ["act"] = "reload_audio",
                ["ids"] = idsStringRepr,
            });

            string text = response.ReadAllText();
            // TODO after 160 data is empty
            JArray dataArray = JsonConvert.DeserializeObject<JToken>(text)["data"][0].ToObject<JArray>();

            List<VkMusicData> result = new List<VkMusicData>();
            foreach (JToken data in dataArray)
            {
                VkMusicData vkMusicData = new VkMusicData
                {
                    Id = data[0].ToString(),
                    OwnderId = data[1].ToString(),
                    Artist = HttpUtility.HtmlDecode(data[4].ToString()),
                    Title = HttpUtility.HtmlDecode(data[3].ToString()),
                    Duration = data[5].ToObject<int>(),
                    Url = data[2].ToString()
                };
                result.Add(vkMusicData);

                // decode
                if (vkMusicData.Url.Contains("audio_api_unavailable") && int.TryParse(vkMusicData.OwnderId, out int ownerId))
                {
                    if (TryDecodeAudioUrl(vkMusicData.Url, ownerId, out string decodedUrl))
                        vkMusicData.Url = decodedUrl;
                }

                // m3u8 to mp3
                if (vkMusicData.Url.Contains("m3u8"))
                {
                    Regex regex = new Regex("/[0-9a-f]+(/audios)?/([0-9a-f]+)/index.m3u8");
                    vkMusicData.Url = regex.Replace(vkMusicData.Url,
                        match => $"{match.Groups[1].Value}/{match.Groups[2].Value}.mp3");
                }
            }
            
            return result;
        }

        private void AddToCollection(IEnumerable<VkMusicData> dataList)
        {
            foreach (VkMusicData data in dataList)
            {
                if (_musicDataById.ContainsKey(data.Id))
                    _musicDataById.Remove(data.Id);
                _musicDataById.Add(data.Id, data);
            }
        }

        private void WaitBeforeNextRequest()
        {
            while (true)
            {
                long delay = _lastRequestTime + _msBetweenRequests - DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                if (delay <= 0)
                    break;
                else
                    Thread.Sleep((int)delay);
            }
            _lastRequestTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        // static
        private static string _userAgent = "Mozilla/5.0 (Windows NT 6.1; rv:52.0) Gecko/20100101 Firefox/52.0";
        private static Cookie[] _defaultCookies = new Cookie[]
        {
            new Cookie("remixaudio_show_alert_today", "0", "/", ".vk.com"),
            new Cookie("remixmdevice", "1920/1080/2/!!-!!!!", "/", ".vk.com")
        };

        private static bool HasSid(List<Cookie> cookies)
        {
            foreach (Cookie c in cookies)
                if (c.Name == "remixsid" || c.Name == "remixsid6")
                    return true;
            return false;
        }

        // vk decode
        private static string VKSTR = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMN0PQRSTUVWXYZO123456789+/=";

        private static bool TryDecodeAudioUrl(string url, int userId, out string decodedUrl)
        {
            string[] vals = url.Split(new string[] { "?extra=" }, StringSplitOptions.None)[1].Split('#');
            string tstr = VK_O(vals[0]);
            string[] opsList = VK_O(vals[1]).Split('\x09');
            Array.Reverse(opsList);

            foreach (string opData in opsList)
            {
                string[] splitOpData = opData.Split('\x0b');
                string cmd = splitOpData[0];
                string arg = null;
                if (splitOpData.Length > 1)
                    arg = splitOpData[1];
                else
                    arg = null;

                if (cmd == "v")
                    tstr = tstr.Reverse();
                else if (cmd == "r")
                    tstr = VK_R(tstr, arg);
                else if (cmd == "x")
                    tstr = VK_XOR(tstr, arg);
                else if (cmd == "s")
                    tstr = VK_S(tstr, int.Parse(arg));
                else if (cmd == "i")
                    tstr = VK_I(tstr, int.Parse(arg), userId);
                else
                {
                    decodedUrl = null;
                    return false;
                }
            }

            decodedUrl = tstr;
            return true;
        }

        private static string VK_O(string val)
        {
            StringBuilder builder = new StringBuilder();
            int shift;
            int i = 0;
            int index2 = 0;
            foreach (char c in val)
            {
                int symIndex = VKSTR.IndexOf(c);
                if (symIndex != -1)
                {
                    if (index2 % 4 != 0)
                        i = (i << 6) + symIndex;
                    else
                        i = symIndex;

                    if (index2 % 4 != 0)
                    {
                        index2 += 1;
                        shift = -2 * index2 & 6;
                        builder.Append((char)(0xFF & (i >> shift)));
                    }
                    else
                        index2 += 1;
                }
            }
            return builder.ToString();
        }

        private static string VK_R(string tstr, string iStr)
        {
            string vkStr2 = VKSTR + VKSTR;
            int i = int.Parse(iStr);

            StringBuilder result = new StringBuilder();
            foreach (char s in tstr)
            {
                int index = vkStr2.IndexOf(s);

                if (index != -1)
                {
                    int offset = index - i;
                    if (offset < 0)
                        offset += vkStr2.Length;
                    result.Append(vkStr2[offset]);
                }
                else
                    result.Append(s);
            }
            return result.ToString();
        }

        private static string VK_XOR(string tstr, string arg)
        {
            int xorVal = arg[0];
            StringBuilder result = new StringBuilder();
            foreach (char c in tstr)
                result.Append((char)(xorVal ^ c));
            return result.ToString();
        }

        private static string VK_I(string t, int e, int userId)
        {
            return VK_S(t, e ^ userId);
        }

        private static string VK_S(string tStr, int e)
        {
            int i = tStr.Length;

            if (i == 0)
                return tStr;

            int[] o = VK_S_CHILD(tStr, e);
            char[] t = tStr.ToCharArray();

            for (int a = 1; a < i; ++a)
            {
                char tm = t[o[i - 1 - a]];
                t[o[i - 1 - a]] = t[a];
                t[a] = tm;
            }

            return new string(t);
        }

        private static int[] VK_S_CHILD(string t, int e)
        {
            int i = t.Length;

            int[] result = new int[t.Length];
            for (int a = t.Length - 1; a > -1; --a)
            {
                e = (i * (a + 1) ^ e + a) % i;
                result[a] = e;
            }

            return result;
        }

    }

    

    
}
