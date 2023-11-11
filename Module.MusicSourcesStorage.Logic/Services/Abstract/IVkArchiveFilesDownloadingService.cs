using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IVkArchiveFilesDownloadingService
{
    /// <returns>Task with result as path to downloaded file.</returns>
    IActivableMultiStepTaskWithProgress<VkArchiveFileDownloadArgs, string> CreateDownloadTask();
}