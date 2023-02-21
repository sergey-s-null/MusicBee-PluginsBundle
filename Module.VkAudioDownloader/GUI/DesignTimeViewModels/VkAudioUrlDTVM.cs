using Module.VkAudioDownloader.GUI.AbstractViewModels;

namespace Module.VkAudioDownloader.GUI.DesignTimeViewModels;

public sealed class VkAudioUrlDTVM : IVkAudioUrlVM
{
    public string Value { get; }
    public bool IsCorrupted { get; }

    public VkAudioUrlDTVM()
    {
        Value = "www.example.com";
        IsCorrupted = false;
    }

    public VkAudioUrlDTVM(string url, bool isCorrupted)
    {
        Value = url;
        IsCorrupted = isCorrupted;
    }
}