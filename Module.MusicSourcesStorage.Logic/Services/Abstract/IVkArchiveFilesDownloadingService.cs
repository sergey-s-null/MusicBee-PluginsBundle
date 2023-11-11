using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IVkArchiveFilesDownloadingService
{
    /// <summary>
    /// Download file from vk archive to specified target path.
    /// </summary>
    /// <param name="document">Vk archive containing downloading file.</param>
    /// <param name="file">Downloading source file.</param>
    /// <param name="targetPath">Path in which file should be downloaded.</param>
    /// <param name="activateTask">Activate returned task.</param>
    /// <param name="token">token</param>
    /// <returns>Downloaded file path.</returns>
    IMultiStepTaskWithProgress<string> DownloadAsync(
        VkDocument document,
        SourceFile file,
        string targetPath,
        bool activateTask,
        CancellationToken token = default
    );
}