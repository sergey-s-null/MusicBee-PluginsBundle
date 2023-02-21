namespace Module.VkAudioDownloader.GUI.AbstractViewModels;

public interface IVkAudioVM
{
    public bool IsSelected { get; set; }

    public long VkId { get; }
    public string Artist { get; }
    public string Title { get; }
    public string Url { get; }
    public bool IsCorruptedUrl { get; }
    public bool IsInIncoming { get; }
}