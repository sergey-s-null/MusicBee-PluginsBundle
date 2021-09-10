using System;

namespace Module.VkMusicDownloader.GUI.VkAudioDownloaderWindow
{
    public class VkAudioVM : BaseAudioVM
    {
        /// <summary>
        /// Последняя добавленная аудиозапись имеет 0 индекс.
        /// </summary>
        public int InsideIndex { get; set; } = -1;
        public bool IsSelected { get; set; } = false;
        public string Url { get; set; } = "";
        public bool IsCorraptedUrl { get; set; } = false;// TODO rename (with xaml)
        
        public override int CompareTo(object obj)
        {
            if (this == obj)
                return 0;
            
            return obj switch
            {
                VkAudioVM other => other.InsideIndex.CompareTo(InsideIndex),
                MBAudioVM => 1,
                BaseAudioVM unknown => throw new NotImplementedException($"Compare of type {unknown.GetType().Name} not implemented."),
                _ => throw new NotSupportedException()
            };
        }
    }
}
