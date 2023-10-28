using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IMusicSourcesStorageService
{
    Task AddMusicSourceAsync(
        VkPostGlobalId postId,
        VkDocumentModel selectedDocument,
        IReadOnlyList<IndexedFile> files,
        CancellationToken token = default
    );
}