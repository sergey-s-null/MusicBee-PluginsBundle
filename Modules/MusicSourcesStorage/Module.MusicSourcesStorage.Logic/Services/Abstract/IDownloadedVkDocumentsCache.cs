using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IDownloadedVkDocumentsCache
{
    bool TryGet(VkOwnedEntityId documentId, out string filePath);

    void Add(VkOwnedEntityId documentId, string filePath);
}