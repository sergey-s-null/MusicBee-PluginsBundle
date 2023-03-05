using VkNet.Abstractions;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;

namespace Module.VkAudioDownloader.Services;

public sealed class AudioCategoryAsyncEnumerableWrapper : IAsyncEnumerable<Audio>
{
    private readonly IAudioCategory _audioCategory;
    private readonly int _audiosPerRequest;

    public AudioCategoryAsyncEnumerableWrapper(
        IAudioCategory audioCategory,
        int audiosPerRequest)
    {
        _audioCategory = audioCategory;
        _audiosPerRequest = audiosPerRequest;
    }

    public IAsyncEnumerator<Audio> GetAsyncEnumerator(CancellationToken cancellationToken = new())
    {
        return new AudiosAsyncEnumerator(
            async (offset, count) =>
                await _audioCategory
                    .GetAsync(new AudioGetParams
                    {
                        Offset = offset,
                        Count = count
                    }),
            _audiosPerRequest
        );
    }

    private delegate Task<IReadOnlyCollection<Audio>> GetAudiosAsyncDelegate(
        int offset,
        int count
    );

    private sealed class AudiosAsyncEnumerator : IAsyncEnumerator<Audio>
    {
        private Audio? _current;

        public Audio Current => _current
                                ?? throw new InvalidOperationException($"{nameof(Current)} is null");

        private readonly GetAudiosAsyncDelegate _getAudiosAsync;
        private readonly int _audiosPerRequest;

        private int _offset;
        private readonly Queue<Audio> _readyAudios = new();

        public AudiosAsyncEnumerator(
            GetAudiosAsyncDelegate getAudiosAsync,
            int audiosPerRequest)
        {
            _getAudiosAsync = getAudiosAsync;
            _audiosPerRequest = audiosPerRequest;
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
            {
                return false;
            }

            _current = _readyAudios.Dequeue();
            return true;
        }

        private async Task<bool> TryLoadNextAudios()
        {
            try
            {
                var audios = await _getAudiosAsync(_offset, _audiosPerRequest);

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
            foreach (var audio in audios)
            {
                _readyAudios.Enqueue(audio);
            }
        }

        public ValueTask DisposeAsync()
        {
            return new ValueTask();
        }
    }
}