using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Exceptions;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IVkService
{
    /// <exception cref="VkServiceException">
    /// Error on get documents
    /// </exception>
    Task<IReadOnlyList<VkDocument>> GetAttachedDocumentsFromPostAsync(
        ulong postOwnerId,
        ulong postId,
        CancellationToken token = default);

    string GetPostGlobalIdString(ulong ownerId, ulong localId);

    string GetPostGlobalIdString(VkPostGlobalId id);
}