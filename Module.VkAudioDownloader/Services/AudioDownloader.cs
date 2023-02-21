using Module.VkAudioDownloader.Entities;
using Module.VkAudioDownloader.Helpers;
using Module.VkAudioDownloader.Services.Abstract;

namespace Module.VkAudioDownloader.Services;

public sealed class AudioDownloader : IAudioDownloader
{
    public async Task<BatchDownloadResult> DownloadBatchAsync(IReadOnlyList<AudioToDownload> audios)
    {
        var tasks = audios
            .Select(DownloadAudioAsync)
            .ToList();

        await Task.WhenAll(tasks);

        return new BatchDownloadResult(
            tasks
                .Select(x => x.Result)
                .ToList()
        );
    }

    private async Task<AudioDownloadResult> DownloadAudioAsync(AudioToDownload audio)
    {
        try
        {
            // todo move this method in separate service or in current class
            await AudioDownloadHelper.DownloadAudioAsync(audio.Url, audio.DestinationPath);
            return new AudioDownloadResult(audio.Url, audio.DestinationPath);
        }
        catch (Exception e)
        {
            return new AudioDownloadResult(audio.Url, audio.DestinationPath, e);
        }
    }
}