using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IVkArchiveFilesDownloadingService
{
    /// <returns>Downloaded file path.</returns>
    IMultiStepTaskWithProgress<string> DownloadAsync(
        VkPostWithArchiveSource source,
        SourceFile file,
        bool activateTask,
        CancellationToken token = default
    );
}