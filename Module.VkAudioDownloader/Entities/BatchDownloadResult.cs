namespace Module.VkAudioDownloader.Entities;

public sealed record BatchDownloadResult(
    IReadOnlyList<AudioDownloadResult> Results
);