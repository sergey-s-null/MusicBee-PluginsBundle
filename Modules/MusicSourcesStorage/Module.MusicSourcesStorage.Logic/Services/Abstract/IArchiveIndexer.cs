using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;
using Module.MusicSourcesStorage.Logic.Exceptions;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IArchiveIndexer
{
    /// <exception cref="ArchiveIndexingException">
    /// Error on indexing archive.
    /// </exception>
    IReadOnlyList<SourceFile> Index(string filePath);

    /// <summary>
    /// Return task with<br/>
    /// - Args (<see cref="string"/>) - path to archive to index;<br/>
    /// - Result (<see cref="IReadOnlyList{T}"/> with <see cref="SourceFile"/>) - indexed files.
    /// </summary>
    IActivableTask<string, IReadOnlyList<SourceFile>> CreateIndexingTask();
}