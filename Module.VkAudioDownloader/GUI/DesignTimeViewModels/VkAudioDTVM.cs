using System.Windows.Input;
using Module.Mvvm.Extension;
using Module.VkAudioDownloader.GUI.AbstractViewModels;

namespace Module.VkAudioDownloader.GUI.DesignTimeViewModels;

public sealed class VkAudioDTVM : IVkAudioVM
{
    public bool IsSelected { get; set; }

    public long VkId { get; }
    public string Artist { get; }
    public string Title { get; }
    public IVkAudioUrlVM? Url { get; }
    public bool IsInIncoming { get; }
    public IReadOnlyList<string> Warnings { get; }

    public ICommand ShowWarnings { get; } = new RelayCommand(_ => { });

    public VkAudioDTVM()
    {
        VkId = 5987612340;
        Artist = "Rick Astley";
        Title = "Never Gonna Give You Up";
        Url = new VkAudioUrlDTVM();
        IsInIncoming = false;
        Warnings = new List<string>();
    }

    public VkAudioDTVM(
        long vkId,
        string artist,
        string title,
        IVkAudioUrlVM? url,
        bool isInIncoming,
        IReadOnlyList<string>? warnings)
    {
        VkId = vkId;
        Artist = artist;
        Title = title;
        Url = url;
        IsInIncoming = isInIncoming;
        Warnings = warnings ?? new List<string>();
    }
}