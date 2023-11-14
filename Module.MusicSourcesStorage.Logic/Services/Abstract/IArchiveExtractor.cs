using Module.MusicSourcesStorage.Logic.Entities.Args;
using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IArchiveExtractor
{
    /// <summary>
    /// Create task that extract single file from archive to specified file path.
    /// </summary>
    /// <returns>Task with result as path to extracted file.</returns>
    IActivableTask<FileExtractionArgs, string> CreateFileExtractionTask();
}