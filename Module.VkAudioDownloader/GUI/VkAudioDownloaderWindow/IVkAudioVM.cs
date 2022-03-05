namespace Module.VkAudioDownloader.GUI.VkAudioDownloaderWindow
{
    public interface IVkAudioVM : IAudioVM
    {
        public bool IsSelected { get; set; }
        
        /// <summary>
        /// Последняя добавленная аудиозапись имеет 0 индекс.
        /// </summary>
        public int InsideIndex { get; }
        public string Url { get; }
        public bool IsCorruptedUrl { get; }
    }
}