using VkNet.Abstractions;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;

namespace Module.VkAudioDownloader.Services;

public sealed class AudioCategoryAsyncEnumerableWrapper : IAsyncEnumerable<Audio>
{
    private readonly IAudioCategory _audioCategory;

    public AudioCategoryAsyncEnumerableWrapper(IAudioCategory audioCategory)
    {
        _audioCategory = audioCategory;
    }

    public IAsyncEnumerator<Audio> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
    {
        return new AudiosAsyncEnumerator(async (offset, count) =>
            await _audioCategory
                .GetAsync(new AudioGetParams
                {
                    Offset = offset,
                    Count = count
                }));
    }

    private delegate Task<IReadOnlyCollection<Audio>> GetAudiosAsyncDelegate(
        int offset,
        int count
    );

    private sealed class AudiosAsyncEnumerator : IAsyncEnumerator<Audio>
    {
        private const int AudiosPerRequest = 10;

        private Audio? _current;

        public Audio Current => _current
                                ?? throw new InvalidOperationException($"{nameof(Current)} is null");

        private readonly GetAudiosAsyncDelegate _getAudiosAsync;
        private int _offset;
        private readonly Queue<Audio> _readyAudios = new();

        public AudiosAsyncEnumerator(GetAudiosAsyncDelegate getAudiosAsync)
        {
            _getAudiosAsync = getAudiosAsync;
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
                var audios = await _getAudiosAsync(_offset, AudiosPerRequest);

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