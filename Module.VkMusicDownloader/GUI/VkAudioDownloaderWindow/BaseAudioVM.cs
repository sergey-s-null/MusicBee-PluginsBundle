using System;
using PropertyChanged;

namespace Module.VkMusicDownloader.GUI.VkAudioDownloaderWindow
{
    [AddINotifyPropertyChangedInterface]
    public abstract class BaseAudioVM : IComparable
    {
        public long VkId { get; set; } = -1;
        public string Artist { get; set; } = "";
        public string Title { get; set; } = "";

        public abstract int CompareTo(object obj);
    }
}
