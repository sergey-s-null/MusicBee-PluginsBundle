using Module.MusicSourcesStorage.Logic.Entities.Args;
using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IVkArchiveFilesDownloadingService
{
    /// <returns>Task with result as path to downloaded file.</returns>
    IActivableMultiStepTask<VkArchiveFileDownloadArgs, string> CreateDownloadTask();
}