using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IVkDocumentDownloader
{
    IActivableTaskWithProgress<VkDocument, string> CreateDownloadTask();
}