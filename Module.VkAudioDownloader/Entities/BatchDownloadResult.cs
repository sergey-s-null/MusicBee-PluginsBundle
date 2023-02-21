namespace Module.VkAudioDownloader.Entities;

public sealed record BatchDownloadResult(
    IReadOnlyCollection<AudioDownloadResult> Results
);