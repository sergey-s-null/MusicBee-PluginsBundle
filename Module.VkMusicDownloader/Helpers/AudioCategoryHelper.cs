using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VkNet.Abstractions;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;
using VkNet.Utils;

namespace Module.VkMusicDownloader.Helpers
{
    public static class AudioCategoryHelper
    {
        public static IEnumerable<Audio> GetIter(this IAudioCategory audioCategory)
        {
            const int audiosPerRequest = 10;
            
            var offset = 0;
            while (true)
            {
                VkCollection<Audio> audios = audioCategory.Get(new AudioGetParams()
                {
                    Offset = offset,
                    Count = audiosPerRequest
                });
                foreach (Audio audio in audios)
                    yield return audio;
                offset += audios.Count;
            }
        }

        public static IAsyncEnumerable<Audio> AsAsyncEnumerable(this IAudioCategory audioCategory)
        {
            return new AsyncEnumerableWrapper(audioCategory);
        }

        private class AsyncEnumerableWrapper : IAsyncEnumerable<Audio>
        {
            private readonly IAudioCategory _audioCategory;
            
            public AsyncEnumerableWrapper(IAudioCategory audioCategory)
            {
                _audioCategory = audioCategory;
            }
            
            public IAsyncEnumerator<Audio> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
            {
                return new AudiosAsyncEnumerator(_audioCategory);
            }
        }
        
        private class AudiosAsyncEnumerator : IAsyncEnumerator<Audio>
        {
            private const int AudiosPerRequest = 10;

            private Audio? _current;
            public Audio Current => _current 
                                    ?? throw new InvalidOperationException($"{nameof(Current)} is null");

            private readonly IAudioCategory _audioCategory;
            private int _offset;
            private readonly Queue<Audio> _readyAudios = new();

            public AudiosAsyncEnumerator(IAudioCategory audioCategory)
            {
                _audioCategory = audioCategory;
            }
            
            public async ValueTask<bool> MoveNextAsync()
            {
                if (_readyAudios.Count == 0)
                {
                    if (!await TryLoadNextAudios())
                    {
                        return false;
                    }
                }

                if (_readyAudios.Count == 0)
                    return false;

                _current = _readyAudios.Dequeue();
                return true;
            }

            private async Task<bool> TryLoadNextAudios()
            {
                try
                {
                    var audios = await _audioCategory.GetAsync(new AudioGetParams
                    {
                        Offset = _offset,
                        Count = AudiosPerRequest
                    });
                    
                    _offset += audios.Count;
                    Enqueue(audios);

                    return true;
                }
                catch
                {
                    return false;
                }
            }
            
            private void Enqueue(IEnumerable<Audio> audios)
            {
                foreach (Audio audio in audios)
                    _readyAudios.Enqueue(audio);
            }
            
            public ValueTask DisposeAsync()
            {
                return new();
            }
        }
    }

    
}
