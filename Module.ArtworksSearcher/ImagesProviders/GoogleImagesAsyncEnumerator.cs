using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Module.ArtworksSearcher.Ex;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Root.Abstractions;

namespace Module.ArtworksSearcher.ImagesProviders
{
    internal class GoogleImagesAsyncEnumerator : IAsyncEnumerator<BitmapImage>
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

            using var webClient = new WebClient();
            try
            {
                var response = await webClient.DownloadStringTaskAsync(url);
                var jObj = JsonConvert.DeserializeObject<JObject>(response);
                var imgUrls = jObj["items"]
                    .Select(item => item["link"].ToString())
                    .ToArray();

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

        #endregion
    }

}