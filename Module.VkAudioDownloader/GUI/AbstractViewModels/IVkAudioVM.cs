using System.Windows.Input;

namespace Module.VkAudioDownloader.GUI.AbstractViewModels;

public interface IVkAudioVM
{
    bool IsSelected { get; set; }
    bool CanBeSelectedForDownloading { get; }

    long VkId { get; }
    string Artist { get; }
    string Title { get; }
    IVkAudioUrlVM? Url { get; }
    bool IsInIncoming { get; }

    IReadOnlyList<string> Warnings { get; }

    ICommand ShowWarnings { get; }
}