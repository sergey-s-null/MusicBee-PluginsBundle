using Module.VkAudioDownloader.GUI.AbstractViewModels;

namespace Module.VkAudioDownloader.GUI.ViewModels
{
    public sealed class MBAudioVM : IMBAudioVM
    {
        public long VkId { get; }
        public string Artist { get; }
        public string Title { get; }

        public int Index { get; }
        public string Index1Str => (Index / 20 + 1).ToString().PadLeft(2, '0');
        public string Index2Str => (Index % 20 + 1).ToString().PadLeft(2, '0');

        public MBAudioVM(long vkId, string artist, string title, int index)
        {
            VkId = vkId;
            Artist = artist;
            Title = title;
            Index = index;
        }
    }
}