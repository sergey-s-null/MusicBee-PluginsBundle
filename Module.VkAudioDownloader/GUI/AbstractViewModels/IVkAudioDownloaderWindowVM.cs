using System.Windows.Input;

namespace Module.VkAudioDownloader.GUI.AbstractViewModels;

public interface IVkAudioDownloaderWindowVM
{
    bool IsDownloading { get; }
    IList<IVkAudioVM> Audios { get; }

    ICommand Refresh { get; }
    ICommand ApplyCheckStateToSelected { get; }
    ICommand Download { get; }
}