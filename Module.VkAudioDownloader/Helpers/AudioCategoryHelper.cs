using Module.VkAudioDownloader.Services;
using VkNet.Abstractions;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;
using VkNet.Utils;

namespace Module.VkAudioDownloader.Helpers;

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
}