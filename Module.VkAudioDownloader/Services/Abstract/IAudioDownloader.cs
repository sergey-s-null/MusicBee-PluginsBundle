using Module.VkAudioDownloader.Entities;

namespace Module.VkAudioDownloader.Services.Abstract;

public interface IAudioDownloader
{
    Task<BatchDownloadResult> DownloadBatchAsync(IReadOnlyList<AudioToDownload> audios);
}