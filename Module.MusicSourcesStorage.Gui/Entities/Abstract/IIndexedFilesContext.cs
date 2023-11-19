using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.Entities.Abstract;

public interface IIndexedFilesContext
{
    IReadOnlyList<SourceFile>? IndexedFiles { get; set; }
}