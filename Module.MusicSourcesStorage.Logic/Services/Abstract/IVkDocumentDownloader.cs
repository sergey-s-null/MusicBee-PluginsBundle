using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IVkDocumentDownloader
{
    IActivableTaskWithProgress<VkDocument, string> CreateDownloadTask();
}