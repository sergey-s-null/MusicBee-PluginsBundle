namespace Module.VkAudioDownloader.GUI.AbstractViewModels
{
    public interface IMBAudioVM : IAudioVM
    {
        public int Index { get; }
        public string Index1Str { get; }
        public string Index2Str { get; }
    }
}