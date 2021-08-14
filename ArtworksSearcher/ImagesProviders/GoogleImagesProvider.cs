using ArtworksSearcher.Ex;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Root.Abstractions;

namespace ArtworksSearcher.ImagesProviders
{
    public class GoogleImagesAsyncEnumerator : IAsyncEnumerator<BitmapImage>
    {
        private const string GoogleApiUrl = "https://www.googleapis.com/customsearch/v1";

        private readonly string _cx;
        private readonly string _key;
        private readonly string _query;

        private int _requestOffset;
        private readonly Queue<string> _urlsQueue = new();
        private readonly List<Task<byte[]>> _downloadingTasks = new();

        public GoogleImagesAsyncEnumerator(string cx, string key, string query)
        {
            _cx = cx;
            _key = key;
            _query = query;
        }

        #region IAsyncEnumerator

        private BitmapImage _current;
        public BitmapImage Current => _current;

        public async Task<bool> MoveNextAsync()
        {
            while (true)
            {
                var parallelTasksCount = Settings.MaxParallelDownloadsCount;
                while (_downloadingTasks.Count < parallelTasksCount)
                {
                    if (_urlsQueue.Count == 0)
                        await TryFillQueueAsync();
                    if (_urlsQueue.Count == 0)
                    {
                        if (_downloadingTasks.Count == 0)
                            return false;
                        
                        break;
                    }

                    while (_downloadingTasks.Count < parallelTasksCount && _urlsQueue.Count > 0)
                    {
                        var webClient = new WebClient();
                        _downloadingTasks.Add(webClient.DownloadDataTaskAsync(_urlsQueue.Dequeue()));
                    }
                }

                var doneTask = await Task.WhenAny(_downloadingTasks);
                _downloadingTasks.Remove(doneTask);
                try
                {
                    var data = await doneTask;
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = new MemoryStream(data);
                    image.EndInit();
                    _current = image;
                    return true;
                }
                catch (Exception)
                {
                    
                }
            }

        }

        private async Task<bool> TryFillQueueAsync()
        {
            var url = UrlEx.AddParameters(GoogleApiUrl, new Dictionary<string, string>
            {
                ["key"] = _key,
                ["cx"] = _cx,
                ["q"] = _query,
                ["searchType"] = "image",
                ["start"] = _requestOffset.ToString()
            });

            using (var webClient = new WebClient())
            {
                try
                {
                    var response = await webClient.DownloadStringTaskAsync(url);
                    var jObj = JsonConvert.DeserializeObject<JObject>(response);
                    var imgUrls = jObj["items"].Select(item => item["link"].ToString()).ToArray();

                    foreach (var imgUrl in imgUrls)
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
        private const string GoogleApiUrl = "https://www.googleapis.com/customsearch/v1";
        private readonly string _cx;
        private readonly string _key;

        public GoogleImagesProvider(string cx, string key)
        {
            _cx = cx;
            _key = key;
        }

        public IEnumerable<BitmapImage> GetImagesIter(string query)
        {
            foreach (var data in GetBinaryIter(query))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = new MemoryStream(data);
                image.EndInit();
                yield return image;
            }
        }

        public IEnumerable<byte[]> GetBinaryIter(string query)
        {
            foreach (var imgUrl in GetImgUrlIter(query))
            {
                using (var webClient = new WebClient())
                {
                    if (webClient.TryDownloadData(imgUrl, out var data))
                        yield return data;
                }
            }
        }

        public IEnumerable<string> GetImgUrlIter(string query)
        {
            var offset = 0;
            while (true)
            {
                var url = UrlEx.AddParameters(GoogleApiUrl, new Dictionary<string, string>
                {
                    ["key"] = _key,
                    ["cx"] = _cx,
                    ["q"] = query,
                    ["searchType"] = "image",
                    ["start"] = offset.ToString()
                });

                using (var webClient = new WebClient())
                {
                    var response = webClient.DownloadString(url);
                    var jObj = JsonConvert.DeserializeObject<JObject>(response);
                    var imgUrls = jObj["items"].Select(item => item["link"].ToString()).ToArray();
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
