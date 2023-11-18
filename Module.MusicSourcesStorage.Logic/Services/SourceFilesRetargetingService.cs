using Module.Core.Helpers;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class SourceFilesRetargetingService : ISourceFilesRetargetingService
{
    private readonly IMusicSourcesStorageService _musicSourcesStorageService;
    private readonly ISourceFilesPathService _sourceFilesPathService;

    public SourceFilesRetargetingService(
        IMusicSourcesStorageService musicSourcesStorageService,
        ISourceFilesPathService sourceFilesPathService)
    {
        _musicSourcesStorageService = musicSourcesStorageService;
        _sourceFilesPathService = sourceFilesPathService;
    }

    public async Task RetargetAsync(
        int sourceId,
        MusicSourceAdditionalInfo oldAdditionalInfo,
        MusicSourceAdditionalInfo newAdditionalInfo,
        CancellationToken token)
    {
        var files = await _musicSourcesStorageService.ListSourceFilesBySourceIdAsync(sourceId, token);

        foreach (var file in files)
        {
            var oldPath = _sourceFilesPathService.GetSourceFileTargetPath(oldAdditionalInfo, file);
            if (!File.Exists(oldPath))
            {
                continue;
            }

            var newPath = _sourceFilesPathService.GetSourceFileTargetPath(newAdditionalInfo, file);
            if (PathHelper.UnifyFilePath(oldPath) == PathHelper.UnifyFilePath(newPath))
            {
                continue;
            }

            var newDirectory = Path.GetDirectoryName(newPath);
            if (newDirectory is not null && !Directory.Exists(newDirectory))
            {
                Directory.CreateDirectory(newDirectory);
            }

            File.Move(oldPath, newPath);
        }

        var oldRoot = _sourceFilesPathService.GetSourceFilesRootDirectory(oldAdditionalInfo);
        DirectoryHelper.DeleteEmpty(oldRoot, true);
    }
}