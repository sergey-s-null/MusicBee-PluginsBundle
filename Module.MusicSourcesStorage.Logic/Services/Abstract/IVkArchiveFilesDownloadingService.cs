using Module.MusicSourcesStorage.Logic.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Entities.Args;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IVkArchiveFilesDownloadingService
{
    /// <returns>Task with result as path to downloaded file.</returns>
    IActivableMultiStepTaskWithProgress<VkArchiveFileDownloadArgs, string> CreateDownloadTask();
}