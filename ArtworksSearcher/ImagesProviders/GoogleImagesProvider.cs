using ArtworksSearcher.Ex;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ArtworksSearcher.ImagesProviders
{
    public class GoogleImagesAsyncEnumerator : IAsyncEnumerator<BitmapImage>
    {
        private const string _googleApiUrl = "https://www.googleapis.com/customsearch/v1";

        private string _cx;
        private string _key;
        private string _query;

        private int _requestOffset = 0;
        private Queue<string> _urlsQueue = new Queue<string>();
        private List<Task<byte[]>> _downloadingTasks = new List<Task<byte[]>>();

        public GoogleImagesAsyncEnumerator(string cx, string key, string query)
        {
            _cx = cx;
            _key = key;
            _query = query;
        }

        #region IAsyncEnumerator

        private BitmapImage _current = null;
        public BitmapImage Current => _current;

        public async Task<bool> MoveNext()
        {
            while (true)
            {
                int parallelTasksCount = Settings.MaxParallelDownloadsCount;
                while (_downloadingTasks.Count < parallelTasksCount)
                {
                    if (_urlsQueue.Count == 0)
                        await TryFillQueueAsync();
                    if (_urlsQueue.Count == 0)
                    {
                        if (_downloadingTasks.Count == 0)
                            return false;
                        else
                            break;
                    }

                    while (_downloadingTasks.Count < parallelTasksCount && _urlsQueue.Count > 0)
                    {
                        WebClient webClient = new WebClient();
                        _downloadingTasks.Add(webClient.DownloadDataTaskAsync(_urlsQueue.Dequeue()));
                    }
                }

                Task<byte[]> doneTask = await Task.WhenAny(_downloadingTasks);
                _downloadingTasks.Remove(doneTask);
                try
                {
                    byte[] data = await doneTask;
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = new MemoryStream(data);
                    image.EndInit();
                    _current = image;
                    return true;
                }
                catch (Exception)
                {
                    continue;
                }
            }

        }

        private async Task<bool> TryFillQueueAsync()
        {
            string url = UrlEx.AddParameters(_googleApiUrl, new Dictionary<string, string>
            {
                ["key"] = _key,
                ["cx"] = _cx,
                ["q"] = _query,
                ["searchType"] = "image",
                ["start"] = _requestOffset.ToString()
            });

            using (WebClient webClient = new WebClient())
            {
                try
                {
                    string response = await webClient.DownloadStringTaskAsync(url);
                    JObject jObj = JsonConvert.DeserializeObject<JObject>(response);
                    string[] imgUrls = jObj["items"].Select(item => item["link"].ToString()).ToArray();

                    foreach (string imgUrl in imgUrls)
                        _urlsQueue.Enqueue(imgUrl);
                    _requestOffset += imgUrls.Length;

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        #endregion
    }

    public class GoogleImagesProvider : IImagesProvider
    {
        private const string _googleApiUrl = "https://www.googleapis.com/customsearch/v1";
        private string _cx;
        private string _key;

        public GoogleImagesProvider(string cx, string key)
        {
            _cx = cx;
            _key = key;
        }

        public IEnumerable<BitmapImage> GetImagesIter(string query)
        {
            foreach (byte[] data in GetBinaryIter(query))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = new MemoryStream(data);
                image.EndInit();
                yield return image;
            }
        }

        public IEnumerable<byte[]> GetBinaryIter(string query)
        {
            foreach (string imgUrl in GetImgUrlIter(query))
            {
                using (WebClient webClient = new WebClient())
                {
                    if (webClient.TryDownloadData(imgUrl, out byte[] data))
                        yield return data;
                }
            }
        }

        public IEnumerable<string> GetImgUrlIter(string query)
        {
            int offset = 0;
            while (true)
            {
                string url = UrlEx.AddParameters(_googleApiUrl, new Dictionary<string, string>
                {
                    ["key"] = _key,
                    ["cx"] = _cx,
                    ["q"] = query,
                    ["searchType"] = "image",
                    ["start"] = offset.ToString()
                });

                using (WebClient webClient = new WebClient())
                {
                    string response = webClient.DownloadString(url);
                    JObject jObj = JsonConvert.DeserializeObject<JObject>(response);
                    string[] imgUrls = jObj["items"].Select(item => item["link"].ToString()).ToArray();
                    foreach (var imgUrl in imgUrls)
                        yield return imgUrl;
                    offset += imgUrls.Length;
                }
            }
        }

        public IAsyncEnumerator<BitmapImage> GetAsyncEnumerator(string query)
        {
            return new GoogleImagesAsyncEnumerator(_cx, _key, query);
        }

    }
}
