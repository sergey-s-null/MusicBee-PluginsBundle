using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IVkService
{
    Task<IReadOnlyList<VkDocument>> GetAttachedDocumentsFromPostAsync(
        ulong postOwnerId,
        ulong postId,
        CancellationToken token = default);
}