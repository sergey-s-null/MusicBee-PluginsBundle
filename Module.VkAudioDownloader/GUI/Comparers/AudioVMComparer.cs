using System;
using System.Collections;
using System.Collections.Generic;
using Module.VkAudioDownloader.GUI.AbstractViewModels;

namespace Module.VkAudioDownloader.GUI.Comparers
{
    public sealed class AudioVMComparer : IComparer<IAudioVM>, IComparer
    {
        public int Compare(object x, object y)
        {
            if (x is IAudioVM xAudio && y is IAudioVM yAudio)
            {
                return Compare(xAudio, yAudio);
            }

            throw new NotSupportedException();
        }

        public int Compare(IAudioVM x, IAudioVM y)
        {
            if (x == y)
            {
                return 0;
            }

            switch (x, y)
            {
                case (IMBAudioVM mbX, IMBAudioVM mbY):
                    return mbX.Index.CompareTo(mbY.Index);
                case (IVkAudioVM vkX, IVkAudioVM vkY):
                    return -1 * vkX.InsideIndex.CompareTo(vkY.InsideIndex);
            }

            var xWeight = GetWeight(x);
            var yWeight = GetWeight(y);

            return xWeight.CompareTo(yWeight);
        }

        private static int GetWeight(IAudioVM audioVM)
        {
            return audioVM switch
            {
                IMBAudioVM => 0,
                IVkAudioVM => 1,
                _ => throw new NotSupportedException()
            };
        }
    }
}