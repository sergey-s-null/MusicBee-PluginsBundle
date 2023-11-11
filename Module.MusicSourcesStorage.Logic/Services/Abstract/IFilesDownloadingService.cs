using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IFilesDownloadingService
{
    /// <param name="fileId">File to download.</param>
    /// <param name="token">Token.</param>
    /// <returns>Task with result as path to downloaded file.</returns>
    Task<IActivableMultiStepTaskWithProgress<string>> CreateFileDownloadTaskAsync(
        int fileId,
        CancellationToken token = default
    );
}