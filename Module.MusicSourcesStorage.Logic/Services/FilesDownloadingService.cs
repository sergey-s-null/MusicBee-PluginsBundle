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

    public FilesDownloadingService(
        IMusicSourcesStorageService musicSourcesStorageService,
        ISourceFilesPathService sourceFilesPathService,
        IVkArchiveFilesDownloadingService vkArchiveFilesDownloadingService)
    {
        _musicSourcesStorageService = musicSourcesStorageService;
        _sourceFilesPathService = sourceFilesPathService;
        _vkArchiveFilesDownloadingService = vkArchiveFilesDownloadingService;
    }

    public async Task<IActivableMultiStepTaskWithProgress<string>> CreateFileDownloadTaskAsync(
        int fileId,
        CancellationToken token)
    {
        var source = await _musicSourcesStorageService.GetMusicSourceByFileIdAsync(fileId, token);
        var file = source.Files.First(x => x.Id == fileId);

        return CreateFileDownloadTask(source, file);
    }

    private IActivableMultiStepTaskWithProgress<string> CreateFileDownloadTask(MusicSource source, SourceFile file)
    {
        if (source is not VkPostWithArchiveSource vkPostWithArchiveSource)
        {
            throw new NotSupportedException("Only vk post with archive source supported.");
        }

        var args = CreateVkArchiveFileDownloadArgs(vkPostWithArchiveSource, file);

        return _vkArchiveFilesDownloadingService
            .CreateDownloadTask()
            .WithArgs(args);
    }

    private VkArchiveFileDownloadArgs CreateVkArchiveFileDownloadArgs(VkPostWithArchiveSource source, SourceFile file)
    {
        var targetPath = _sourceFilesPathService.GetSourceFileTargetPath(source.AdditionalInfo, file);
        return new VkArchiveFileDownloadArgs(source.Document, file, targetPath);
    }
}