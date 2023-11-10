using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IFilesDownloadingService
{
    /// <returns>Downloaded file path.</returns>
    IMultiStepTaskWithProgress<string> DownloadAsync(
        int fileId,
        bool activateTask,
        CancellationToken token = default
    );
}