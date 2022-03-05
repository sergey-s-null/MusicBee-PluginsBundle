using System.Collections.Generic;
using System.Windows.Input;

namespace Module.VkAudioDownloader.GUI.VkAudioDownloaderWindow
{
    public interface IVkAudioDownloaderWindowVM
    {
        ICommand RefreshCmd { get; }
        ICommand ApplyCheckStateToSelectedCmd { get; }
        bool IsDownloading { get; }
        ICommand DownloadCmd { get; }
        IList<IAudioVM> Audios { get; }
    }
}