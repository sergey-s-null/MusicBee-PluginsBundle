using Module.VkAudioDownloader.GUI.AbstractViewModels;

namespace Module.VkAudioDownloader.GUI.DesignTimeViewModels;

public sealed class VkAudioDTVM : IVkAudioVM
{
    public bool IsSelected { get; set; } = true;

    public long VkId { get; }
    public string Artist { get; }
    public string Title { get; }
    public string Url { get; }
    public bool IsCorruptedUrl { get; }

    public VkAudioDTVM()
    {
        VkId = 5987612340;
        Artist = "Rick Astley";
        Title = "Never Gonna Give You Up";
        Url = "www.example.com";
        IsCorruptedUrl = false;
    }

    public VkAudioDTVM(
        long vkId,
        string artist,
        string title,
        string url,
        bool isCorruptedUrl)
    {
        VkId = vkId;
        Artist = artist;
        Title = title;
        Url = url;
        IsCorruptedUrl = isCorruptedUrl;
    }
}