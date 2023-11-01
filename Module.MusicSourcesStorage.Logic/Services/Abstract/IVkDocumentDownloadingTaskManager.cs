using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IVkDocumentDownloadingTaskManager
{
    IFileDownloadingTask GetOrStartNew(VkDocument document);

    void CancelDownloading(VkDocument document);
}