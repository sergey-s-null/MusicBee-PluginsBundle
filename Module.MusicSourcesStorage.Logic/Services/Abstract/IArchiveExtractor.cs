using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IArchiveExtractor
{
    /// <summary>
    /// Extract single file from archive to specified file path.
    /// </summary>
    /// <param name="archiveFilePath">Path to archive extract file from.</param>
    /// <param name="filePathInArchive">Relative path to file inside archive to extract.</param>
    /// <param name="targetFilePath">Path to file in which file should be extracted.</param>
    /// <param name="activateTask">Activate task after creation.</param>
    /// <param name="token">cancellation token.</param>
    /// <returns>Path to extracted file.</returns>
    ITaskWithProgress<string> ExtractAsync(
        string archiveFilePath,
        string filePathInArchive,
        string targetFilePath,
        bool activateTask,
        CancellationToken token = default
    );
}