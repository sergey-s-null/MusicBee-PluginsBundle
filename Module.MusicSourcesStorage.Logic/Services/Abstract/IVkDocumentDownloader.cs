using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Exceptions;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IVkDocumentDownloader
{
    /// <summary>
    /// Download file to special folder.
    /// </summary>
    /// <returns>
    /// Path to downloaded file.
    /// </returns>
    /// <exception cref="VkDocumentDownloadingException">
    /// Error on downloading. 
    /// </exception>
    Task<string> DownloadAsync(VkDocument document, CancellationToken token = default);
}