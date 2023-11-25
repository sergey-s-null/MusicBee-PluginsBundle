using Module.MusicSourcesStorage.Logic.Entities.Args;
using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IVkArchiveFilesDownloadingService
{
    /// <summary>
    /// Return task with<br/>
    /// - Args (<see cref="VkArchiveFileDownloadArgs"/>) - file downloading args;<br/>
    /// - Result (<see cref="string"/>) - downloaded file path.
    /// </summary>
    IActivableMultiStepTask<VkArchiveFileDownloadArgs, string> CreateDownloadTask();
}