using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IVkDocumentDownloadingTaskManager
{
    /// <returns>Path to downloaded file.</returns>
    ITaskWithProgress<string> GetOrCreateNewAsync(
        VkDocument document,
        bool activateTask,
        CancellationToken token = default
    );
}