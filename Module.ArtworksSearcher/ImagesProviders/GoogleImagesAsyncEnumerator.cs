using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Module.ArtworksSearcher.Exceptions;
using Module.ArtworksSearcher.Services.Abstract;
using Module.ArtworksSearcher.Settings;
using MoreLinq;

namespace Module.ArtworksSearcher.ImagesProviders
{
    public class GoogleImagesAsyncEnumerator : IAsyncEnumerator<BitmapImage>
    {
        private CancellationToken _cancellationToken;

        private readonly string _query;
        private readonly int _parallelTasksCount;

        private int _requestOffset;
        private readonly Queue<string> _urlsQueue = new();
        private readonly List<Task<byte[]>> _downloadingTasks = new();

        public BitmapImage Current => _current
                                      ?? throw new InvalidOperationException($"{nameof(Current)} is null.");

        private BitmapImage? _current;


        private readonly IGoogleImageSearchService _googleImageSearchService;

        public GoogleImagesAsyncEnumerator(
            string query,
            CancellationToken cancellationToken,
            // DI
            IArtworksSearcherSettings settings,
            IGoogleImageSearchService googleImageSearchService)
        {
            _query = query;
            _cancellationToken = cancellationToken;

            _parallelTasksCount = settings.ParallelDownloadsCount;
            _googleImageSearchService = googleImageSearchService;
        }

        public async ValueTask<bool> MoveNextAsync()
        {
            while (true)
            {
                while (_downloadingTasks.Count < _parallelTasksCount)
                {
                    if (_urlsQueue.Count == 0)
                        await FillImageUrlsQueueAsync();
                    if (_urlsQueue.Count == 0)
                    {
                        if (_downloadingTasks.Count == 0)
                            return false;

                        break;
                    }

                    StartDownloadingTasksIfPossible();
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

        private async Task FillImageUrlsQueueAsync()
        {
            IReadOnlyCollection<string> imageUrls;
            try
            {
                imageUrls = await _googleImageSearchService.SearchAsync(_query, _requestOffset, _cancellationToken);
            }
            catch (GoogleSearchImageException e)
            {
                // todo make error displaying (sometime)
                Console.WriteLine(e);
                return;
            }

            imageUrls.ForEach(x => _urlsQueue.Enqueue(x!));
            _requestOffset += imageUrls.Count;
        }

        private void StartDownloadingTasksIfPossible()
        {
            while (_downloadingTasks.Count < _parallelTasksCount && _urlsQueue.Count > 0)
            {
                var webClient = new WebClient();
                _cancellationToken.Register(webClient.CancelAsync);
                _downloadingTasks.Add(webClient.DownloadDataTaskAsync(_urlsQueue.Dequeue()));
            }
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
            return new ValueTask();
        }
    }
}