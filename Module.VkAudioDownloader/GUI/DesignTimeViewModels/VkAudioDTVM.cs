using Module.VkAudioDownloader.GUI.AbstractViewModels;

namespace Module.VkAudioDownloader.GUI.DesignTimeViewModels;

public sealed class VkAudioDTVM : IVkAudioVM
{
    public bool IsSelected { get; set; }

    public long VkId { get; }
    public string Artist { get; }
    public string Title { get; }
    public string Url { get; }
    public bool IsCorruptedUrl { get; }
    public bool IsInIncoming { get; }

    public VkAudioDTVM()
    {
        VkId = 5987612340;
        Artist = "Rick Astley";
        Title = "Never Gonna Give You Up";
        Url = "www.example.com";
        IsCorruptedUrl = false;
        IsInIncoming = false;
    }

    public VkAudioDTVM(
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