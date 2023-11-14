using Module.MusicSourcesStorage.Logic.Entities.Args;
using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;
using Module.MusicSourcesStorage.Logic.Extensions;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class VkArchiveFilesDownloadingService : IVkArchiveFilesDownloadingService
{
    private readonly IVkDocumentDownloadingTaskManager _vkDocumentDownloadingTaskManager;
    private readonly IArchiveExtractor _archiveExtractor;

    public VkArchiveFilesDownloadingService(
        IVkDocumentDownloadingTaskManager vkDocumentDownloadingTaskManager,
        IArchiveExtractor archiveExtractor)
    {
        _vkDocumentDownloadingTaskManager = vkDocumentDownloadingTaskManager;
        _archiveExtractor = archiveExtractor;
    }

    public IActivableMultiStepTask<VkArchiveFileDownloadArgs, string> CreateDownloadTask()
    {
        var archiveDownloadingTask = _vkDocumentDownloadingTaskManager.CreateDownloadTask();
        var fileExtractionTask = _archiveExtractor.CreateFileExtractionTask();

        return archiveDownloadingTask
            .ChangeArgs((VkArchiveFileDownloadArgs args) => args.Archive)
            .Chain(
                (args, archiveFilePath) => new FileExtractionArgs(
                    archiveFilePath,
                    args.SourceFile.Path,
                    args.TargetFilePath,
                    true
                ),
                fileExtractionTask
            );
    }
}