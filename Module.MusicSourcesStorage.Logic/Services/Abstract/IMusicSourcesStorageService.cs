using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IMusicSourcesStorageService
{
    Task AddMusicSourceAsync(
        VkPostGlobalId postId,
        VkDocument selectedDocument,
        IReadOnlyList<IndexedFile> files,
        CancellationToken token = default
    );
}