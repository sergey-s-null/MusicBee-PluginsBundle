using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IArchiveExtractor
{
    /// <summary>
    /// Create task that extract single file from archive to specified file path.
    /// </summary>
    IActivableTaskWithProgress<FileExtractionArgs, string> CreateFileExtractionTask();
}