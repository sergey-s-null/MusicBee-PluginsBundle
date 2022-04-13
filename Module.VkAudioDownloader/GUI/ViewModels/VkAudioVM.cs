using Module.VkAudioDownloader.GUI.AbstractViewModels;
using PropertyChanged;

namespace Module.VkAudioDownloader.GUI.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class VkAudioVM : IVkAudioVM
    {
        public long VkId { get; }
        public string Artist { get; }
        public string Title { get; }

        public bool IsSelected { get; set; }
        public int InsideIndex { get; }
        public string Url { get; }
        public bool IsCorruptedUrl { get; }

        public VkAudioVM(
            long vkId, 
            string artist, 
            string title,
            int insideIndex, 
            string url, 
            bool isCorruptedUrl)
        {
            VkId = vkId;
            Artist = artist;
            Title = title;
            InsideIndex = insideIndex;
            Url = url;
            IsCorruptedUrl = isCorruptedUrl;
        }
    }
}