using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Exceptions;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IArchiveIndexer
{
    /// <exception cref="ArchiveIndexingException">
    /// Error on indexing archive.
    /// </exception>
    IReadOnlyList<IndexedFile> Index(string filePath);
}