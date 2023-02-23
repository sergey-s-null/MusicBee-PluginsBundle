namespace Module.VkAudioDownloader.Entities;

public sealed record AudioDownloadResult(
    string Url,
    string DestinationPath,
    Exception? Exception = null
)
{
    public bool IsSuccess => Exception is null;
}