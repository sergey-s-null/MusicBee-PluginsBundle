using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Extensions;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class FilesDownloadingService : IFilesDownloadingService
{
    private readonly IMusicSourcesStorageService _musicSourcesStorageService;
    private readonly IVkArchiveFilesDownloadingService _vkArchiveFilesDownloadingService;

    public FilesDownloadingService(
        IMusicSourcesStorageService musicSourcesStorageService,
        IVkArchiveFilesDownloadingService vkArchiveFilesDownloadingService)
    {
        _musicSourcesStorageService = musicSourcesStorageService;
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

        return _vkArchiveFilesDownloadingService.DownloadAsync(
            vkPostWithArchiveSource.AdditionalInfo,
            vkPostWithArchiveSource.Document,
            vkPostWithArchiveSource.Files.First(x => x.Id == fileId),
            activateTask,
            token
        );
    }
}