using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Extensions;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class VkArchiveFilesDownloadingService : IVkArchiveFilesDownloadingService
{
    private readonly ISourceFilesPathService _sourceFilesPathService;
    private readonly IVkDocumentDownloadingTaskManager _vkDocumentDownloadingTaskManager;
    private readonly IArchiveExtractor _archiveExtractor;

    public VkArchiveFilesDownloadingService(
        ISourceFilesPathService sourceFilesPathService,
        IVkDocumentDownloadingTaskManager vkDocumentDownloadingTaskManager,
        IArchiveExtractor archiveExtractor)
    {
        _sourceFilesPathService = sourceFilesPathService;
        _vkDocumentDownloadingTaskManager = vkDocumentDownloadingTaskManager;
        _archiveExtractor = archiveExtractor;
    }

    public IMultiStepTaskWithProgress<string> DownloadAsync(
        VkPostWithArchiveSource source,
        SourceFile file,
        bool activateTask,
        CancellationToken token = default)
    {
        var targetFilePath = _sourceFilesPathService.GetDownloadingFilePath(source.AdditionalInfo, file);

        var task = _vkDocumentDownloadingTaskManager.GetOrCreateNewAsync(source.Document, false, token)
            .Chain(archiveFilePath => _archiveExtractor.ExtractAsync(
                archiveFilePath,
                file.Path,
                targetFilePath,
                true,
                false,
                token
            ));

        if (activateTask)
        {
            task.Activate();
        }

        return task;
    }
}