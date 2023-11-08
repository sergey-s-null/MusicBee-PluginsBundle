using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface ISourceFilesPathService
{
    /// <summary>
    /// Return path in which source file should be downloaded.
    /// </summary>
    string GetDownloadingFilePath(MusicSource source, SourceFile file);
}