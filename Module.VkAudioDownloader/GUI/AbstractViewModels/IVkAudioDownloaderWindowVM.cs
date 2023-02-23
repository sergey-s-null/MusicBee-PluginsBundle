using System.Windows.Input;

namespace Module.VkAudioDownloader.GUI.AbstractViewModels;

public interface IVkAudioDownloaderWindowVM
{
    bool IsRefreshing { get; }
    bool IsDownloading { get; }
    IList<IVkAudioVM> Audios { get; }

    ICommand Refresh { get; }
    ICommand Download { get; }
}