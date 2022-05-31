using Module.AudioSourcesComparer.GUI.AbstractViewModels;

namespace Module.AudioSourcesComparer.GUI.DesignTimeViewModels
{
    public class MBAudioDTVM : IMBAudioVM
    {
        public int VkId { get; }
        public int Index { get; }
        public string Artist { get; }
        public string Title { get; }

        public MBAudioDTVM()
        {
            VkId = 456240678;
            Index = 1002;
            Artist = "Default Artist";
            Title = "Default Title";
        }

        public MBAudioDTVM(int vkId, int index, string artist, string title)
        {
            VkId = vkId;
            Index = index;
            Artist = artist;
            Title = title;
        }
    }
}