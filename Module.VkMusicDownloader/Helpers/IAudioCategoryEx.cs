using System.Collections.Generic;
using System.Threading.Tasks;
using Module.VkMusicDownloader.Abstractions;
using VkNet.Abstractions;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;
using VkNet.Utils;

namespace Module.VkMusicDownloader.Helpers
{
    public static class IAudioCategoryEx
    {
        public static IEnumerable<Audio> GetIter(this IAudioCategory audioCategory)
        {
            int offset = 0;
            int audiosPerRequest = 10;
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

        public static IAsyncEnumerator<Audio> GetAsyncEnumerator(this IAudioCategory audioCategory)
        {
            return new AudiosAsyncEnumerator(audioCategory);
        }

        private class AudiosAsyncEnumerator : IAsyncEnumerator<Audio>
        {
            private const int _audiosPerRequest = 10;

            private Audio _current = null;
            public Audio Current => _current;

            private IAudioCategory _audioCategory;
            private int _offset = 0;
            private Queue<Audio> _readyAudios = new Queue<Audio>();

            public AudiosAsyncEnumerator(IAudioCategory audioCategory)
            {
                _audioCategory = audioCategory;
            }

            public async Task<bool> MoveNextAsync()
            {
                if (_readyAudios.Count == 0)
                {
                    VkCollection<Audio> audios;
                    try
                    {
                        audios = await _audioCategory.GetAsync(new AudioGetParams()
                        {
                            Offset = _offset,
                            Count = _audiosPerRequest
                        });
                    }
                    catch
                    {
                        return false;
                    }
                    
                    _offset += audios.Count;
                    foreach (Audio audio in audios)
                        _readyAudios.Enqueue(audio);
                }

                if (_readyAudios.Count == 0)
                    return false;

                _current = _readyAudios.Dequeue();
                return true;
            }


        }

    }
}
