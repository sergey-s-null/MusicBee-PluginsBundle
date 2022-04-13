namespace Module.VkAudioDownloader.GUI.AbstractViewModels
{
    public interface IAudioVM
    {
        public long VkId { get; }
        public string Artist { get; }
        public string Title { get; }
    }
}
