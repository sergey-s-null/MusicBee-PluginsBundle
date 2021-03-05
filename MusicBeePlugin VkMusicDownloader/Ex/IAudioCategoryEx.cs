using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet.Abstractions;
using VkNet.Model.Attachments;
using VkNet.Utils;
using VkNet.Model.RequestParams;

namespace VkMusicDownloader.Ex
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
    }
}
