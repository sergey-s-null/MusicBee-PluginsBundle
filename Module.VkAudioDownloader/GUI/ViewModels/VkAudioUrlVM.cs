using Module.VkAudioDownloader.GUI.AbstractViewModels;

namespace Module.VkAudioDownloader.GUI.ViewModels;

public sealed class VkAudioUrlVM : IVkAudioUrlVM
{
    public string Value { get; }
    public bool IsCorrupted { get; }

    public VkAudioUrlVM(string url, bool isCorrupted)
    {
        Value = url;
        IsCorrupted = isCorrupted;
    }
}