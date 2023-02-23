namespace Module.VkAudioDownloader.Entities;

public sealed record VkAudioModel(
    long Id,
    string Artist,
    string Title,
    string Url,
    bool IsInIncoming
);