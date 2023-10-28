using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Exceptions;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IVkService
{
    /// <exception cref="VkServiceException">
    /// Error on get documents
    /// </exception>
    Task<IReadOnlyList<VkDocumentModel>> GetAttachedDocumentsFromPostAsync(
        long postOwnerId,
        long postId,
        CancellationToken token = default);

    string GetPostGlobalIdString(long ownerId, long localId);

    string GetPostGlobalIdString(VkPostGlobalId id);
}