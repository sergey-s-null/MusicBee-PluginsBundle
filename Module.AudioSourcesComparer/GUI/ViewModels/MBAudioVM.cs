using Module.AudioSourcesComparer.GUI.AbstractViewModels;

namespace Module.AudioSourcesComparer.GUI.ViewModels
{
    public class MBAudioVM : IMBAudioVM
    {
        public long VkId { get; }
        public int Index { get; }
        public string Artist { get; }
        public string Title { get; }

        public MBAudioVM(long vkId, int index, string artist, string title)
        {
            VkId = vkId;
            Index = index;
            Artist = artist;
            Title = title;
        }
    }
}