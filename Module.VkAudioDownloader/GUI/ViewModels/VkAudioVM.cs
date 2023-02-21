using Module.VkAudioDownloader.GUI.AbstractViewModels;
using PropertyChanged;

namespace Module.VkAudioDownloader.GUI.ViewModels;

[AddINotifyPropertyChangedInterface]
public sealed class VkAudioVM : IVkAudioVM
{
    public bool IsSelected { get; set; }

    public long VkId { get; }
    public string Artist { get; }
    public string Title { get; }
    public string Url { get; }
    public bool IsCorruptedUrl { get; }
    public bool IsInIncoming { get; }

    public VkAudioVM(
        long vkId,
        string artist,
        string title,
        string url,
        bool isCorruptedUrl,
        bool isInIncoming)
    {
        VkId = vkId;
        Artist = artist;
        Title = title;
        Url = url;
        IsCorruptedUrl = isCorruptedUrl;
        IsInIncoming = isInIncoming;
    }
}