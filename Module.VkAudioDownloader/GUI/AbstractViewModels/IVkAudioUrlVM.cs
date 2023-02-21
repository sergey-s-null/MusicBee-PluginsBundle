namespace Module.VkAudioDownloader.GUI.AbstractViewModels;

public interface IVkAudioUrlVM
{
    string Value { get; }
    bool IsCorrupted { get; }
}