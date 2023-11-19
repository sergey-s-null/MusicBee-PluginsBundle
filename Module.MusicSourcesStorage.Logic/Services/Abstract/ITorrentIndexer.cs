using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface ITorrentIndexer
{
    /// <summary>
    /// Return task with<br/>
    /// - Args (<see cref="string"/>) - path to torrent file;<br/>
    /// - Result (<see cref="IReadOnlyList{T}"/> with <see cref="SourceFile"/>) - indexed files.
    /// </summary>
    IActivableTask<string, IReadOnlyList<SourceFile>> CreateIndexingTask();
}