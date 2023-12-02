using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IVkDocumentDownloadingTaskManager
{
    /// <summary>
    /// Return task with<br/>
    /// - Args (<see cref="VkDocument"/>) - document to download;<br/>
    /// - Result (<see cref="string"/>) - downloaded file path.
    /// </summary>
    /// <returns></returns>
    IActivableTask<VkDocument, string> CreateDownloadTask();
}