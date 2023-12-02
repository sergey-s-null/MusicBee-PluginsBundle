using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Args;
using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IFilesDownloadingService
{
    /// <param name="fileId">File to download.</param>
    /// <param name="token">Token.</param>
    /// <returns>Task with result as path to downloaded file.</returns>
    Task<IActivableMultiStepTask<FileDownloadArgs, string>> CreateFileDownloadTaskAsync(
        int fileId,
        CancellationToken token = default
    );

    /// <param name="file">File to download.</param>
    /// <param name="token">Token.</param>
    /// <returns>Task with result as path to downloaded file.</returns>
    Task<IActivableMultiStepTask<FileDownloadArgs, string>> CreateFileDownloadTaskAsync(
        SourceFile file,
        CancellationToken token = default
    );
}