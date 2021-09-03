using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Module.ArtworksSearcher.Helpers;
using Module.ArtworksSearcher.Settings;
using MoreLinq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Root.Helpers;

namespace Module.ArtworksSearcher.ImagesProviders
{
    // TODO подумоть над интёрнализацией
    public class GoogleImagesAsyncEnumerator : IAsyncEnumerator<BitmapImage>
    {
        private const string GoogleApiUrl = "https://www.googleapis.com/customsearch/v1";

        private CancellationToken _cancellationToken;
        
        private readonly string _cx;
        private readonly string _key;
        private readonly string _query;
        private readonly int _parallelTasksCount;

        private int _requestOffset;
        private readonly Queue<string> _urlsQueue = new();
        private readonly List<Task<byte[]>> _downloadingTasks = new();

        private BitmapImage? _current;
        public BitmapImage Current => _current 
                                      ?? throw new InvalidOperationException($"{nameof(Current)} is null.");
        
        public GoogleImagesAsyncEnumerator(
            string query, 
            CancellationToken cancellationToken,
            // DI
            IArtworksSearcherSettings settings)
        {
            _cancellationToken = cancellationToken;
            
            _cx = settings.GoogleCX;
            _key = settings.GoogleKey;
            _query = query;
            _parallelTasksCount = settings.ParallelDownloadsCount;
        }
        
        public async ValueTask<bool> MoveNextAsync()
        {
            // TODO refactor?
            while (true)
            {
                while (_downloadingTasks.Count < _parallelTasksCount)
                {
                    if (_urlsQueue.Count == 0)
                        await FillQueueAsync();
                    if (_urlsQueue.Count == 0)
                    {
                        if (_downloadingTasks.Count == 0)
                            return false;
                        
                        break;
                    }

                    while (_downloadingTasks.Count < _parallelTasksCount && _urlsQueue.Count > 0)
                    {
                        var webClient = new WebClient();
                        _cancellationToken.Register(webClient.CancelAsync);
                        _downloadingTasks.Add(webClient.DownloadDataTaskAsync(_urlsQueue.Dequeue()));
                    }
                }

                var doneTask = await Task.WhenAny(_downloadingTasks);
                _downloadingTasks.Remove(doneTask);
                try
                {
                    _current = BinaryToImage(await doneTask);
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private async Task FillQueueAsync()
        {
            var url = GetPreparedUrl();

            using var webClient = new WebClient();
            using var tokenRegistration = _cancellationToken.Register(webClient.CancelAsync);
            try
            {
                var response = await webClient.DownloadStringTaskAsync(url);
                var jObj = JsonConvert.DeserializeObject<JObject>(response);

                var imgUrls = jObj?["items"]?
                    .Select(item => item["link"]?.ToString())
                    .ToReadOnlyCollection();
                
                if (imgUrls is not null)
                {
                    imgUrls
                        .Where(x => x is not null)
                        .ForEach(x => _urlsQueue.Enqueue(x!));
                    
                    _requestOffset += imgUrls.Count;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private string GetPreparedUrl()
        { 
            return UrlHelper.AddParameters(GoogleApiUrl, new Dictionary<string, string>
            {
                ["key"] = _key,
                ["cx"] = _cx,
                ["q"] = _query,
                ["searchType"] = "image",
                ["start"] = _requestOffset.ToString()
            });
        }

        private static BitmapImage BinaryToImage(byte[] binaryImage)
        {
            var image = new BitmapImage();
            
            image.BeginInit();
            image.StreamSource = new MemoryStream(binaryImage);
            image.EndInit();
            
            return image;
        }

        public ValueTask DisposeAsync()
        {
            return new();
        }
    }

}