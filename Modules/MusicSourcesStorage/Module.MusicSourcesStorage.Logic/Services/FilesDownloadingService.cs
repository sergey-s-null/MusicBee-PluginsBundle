using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Args;
using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;
using Module.MusicSourcesStorage.Logic.Extensions;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class FilesDownloadingService : IFilesDownloadingService
{
    private readonly IMusicSourcesStorageService _musicSourcesStorageService;
    private readonly ISourceFilesPathService _sourceFilesPathService;
    private readonly IVkArchiveFilesDownloadingService _vkArchiveFilesDownloadingService;
    private readonly INewFileInitializationService _newFileInitializationService;

    public FilesDownloadingService(
        IMusicSourcesStorageService musicSourcesStorageService,
        ISourceFilesPathService sourceFilesPathService,
        IVkArchiveFilesDownloadingService vkArchiveFilesDownloadingService, 
        INewFileInitializationService newFileInitializationService)
    {
        _musicSourcesStorageService = musicSourcesStorageService;
        _sourceFilesPathService = sourceFilesPathService;
        _vkArchiveFilesDownloadingService = vkArchiveFilesDownloadingService;
        _newFileInitializationService = newFileInitializationService;
    }

    public async Task<IActivableMultiStepTask<FileDownloadArgs, string>> CreateFileDownloadTaskAsync(
        int fileId,
        CancellationToken token)
    {
        var source = await _musicSourcesStorageService.GetMusicSourceByFileIdAsync(fileId, token);
        var file = source.Files.First(x => x.Id == fileId);

        return CreateFileDownloadTask(source, file);
    }

    public async Task<IActivableMultiStepTask<FileDownloadArgs, string>> CreateFileDownloadTaskAsync(
        SourceFile file,
        CancellationToken token)
    {
        var source = await _musicSourcesStorageService.GetMusicSourceByFileIdAsync(file.Id, token);

        return CreateFileDownloadTask(source, file);
    }

    private IActivableMultiStepTask<FileDownloadArgs, string> CreateFileDownloadTask(
        MusicSource source,
        SourceFile file)
    {
        if (source is not VkPostWithArchiveSource vkPostWithArchiveSource)
        {
            throw new NotSupportedException("Only vk post with archive source supported.");
        }

        var targetPath = _sourceFilesPathService.GetSourceFileTargetPath(source.AdditionalInfo, file);

        var task = _vkArchiveFilesDownloadingService.CreateDownloadTask();

        if (file is MusicFile)
        {
            task = task.Chain(
                (_, filePath) => new NewFileInitializationArgs(file.Id, filePath),
                _newFileInitializationService.CreateTask(),
                (_, downloadedFilePath, _) => downloadedFilePath
            );
        }

        return task
            .ChangeArgs((FileDownloadArgs args) => new VkArchiveFileDownloadArgs(
                vkPostWithArchiveSource.Document,
                file,
                args.SkipIfDownloaded,
                targetPath
            ));
    }
}