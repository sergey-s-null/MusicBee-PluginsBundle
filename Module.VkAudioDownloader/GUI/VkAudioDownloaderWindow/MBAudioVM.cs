using System;

namespace Module.VkAudioDownloader.GUI.VkAudioDownloaderWindow
{
    public class MBAudioVM : BaseAudioVM
    {
        #region Bindings

        public int Index { get; set; } = -1;// TODO проверить как Fody работает с наследованием

        public string Index1Str => (Index / 20 + 1).ToString().PadLeft(2, '0');
        public string Index2Str => (Index % 20 + 1).ToString().PadLeft(2, '0');

        #endregion

        public override int CompareTo(object obj)
        {
            if (this == obj)
                return 0;
            
            return obj switch
            {
                MBAudioVM other => Index.CompareTo(other.Index),
                VkAudioVM => -1,
                BaseAudioVM unknown => throw new NotImplementedException($"Compare of type {unknown.GetType().Name} not implemented."),
                _ => throw new NotSupportedException()
            };
        }
    }
}
