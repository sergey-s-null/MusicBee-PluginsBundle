using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Extensions;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class FilesDownloadingService : IFilesDownloadingService
{
    private readonly IMusicSourcesStorageService _musicSourcesStorageService;
    private readonly ISourceFilesPathService _sourceFilesPathService;
    private readonly IVkArchiveFilesDownloadingService _vkArchiveFilesDownloadingService;

    public FilesDownloadingService(
        IMusicSourcesStorageService musicSourcesStorageService,
        ISourceFilesPathService sourceFilesPathService,
        IVkArchiveFilesDownloadingService vkArchiveFilesDownloadingService)
    {
        _musicSourcesStorageService = musicSourcesStorageService;
        _sourceFilesPathService = sourceFilesPathService;
        _vkArchiveFilesDownloadingService = vkArchiveFilesDownloadingService;
    }

    public IMultiStepTaskWithProgress<string> DownloadAsync(int fileId, bool activateTask, CancellationToken token)
    {
        var getSourceTask = new DefaultTaskWithProgress<MusicSource>(
            internalToken => _musicSourcesStorageService.GetMusicSourceByFileIdAsync(fileId, internalToken).Result,
            token
        );

        var task = getSourceTask.Chain(x => DownloadInternalAsync(x, fileId, false, token));

        if (activateTask)
        {
            task.Activate();
        }

        return task;
    }

    private IMultiStepTaskWithProgress<string> DownloadInternalAsync(
        MusicSource source,
        int fileId,
        bool activateTask,
        CancellationToken token)
    {
        if (source is not VkPostWithArchiveSource vkPostWithArchiveSource)
        {
            throw new NotSupportedException("Only vk post with archive source supported.");
        }

        var sourceFile = source.Files.First(x => x.Id == fileId);

        var targetPath = _sourceFilesPathService.GetSourceFileTargetPath(source.AdditionalInfo, sourceFile);

        return _vkArchiveFilesDownloadingService.DownloadAsync(
            vkPostWithArchiveSource.Document,
            sourceFile,
            targetPath,
            activateTask,
            token
        );
    }
}