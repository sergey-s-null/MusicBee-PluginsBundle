using Module.MusicSourcesStorage.Logic.Enums;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class FilesLocatingService : IFilesLocatingService
{
    private readonly ISourceFilesPathService _sourceFilesPathService;
    private readonly IMusicSourcesStorageService _musicSourcesStorageService;

    public FilesLocatingService(
        ISourceFilesPathService sourceFilesPathService,
        IMusicSourcesStorageService musicSourcesStorageService)
    {
        _sourceFilesPathService = sourceFilesPathService;
        _musicSourcesStorageService = musicSourcesStorageService;
    }

    public async Task<string?> LocateFileAsync(int fileId, CancellationToken token)
    {
        var sourceFile = await _musicSourcesStorageService.GetSourceFileAsync(fileId, token);

        var additionalInfo = await _musicSourcesStorageService.GetAdditionalInfoByIdAsync(sourceFile.SourceId, token);

        var filePath = _sourceFilesPathService.GetSourceFileTargetPath(additionalInfo, sourceFile);
        return File.Exists(filePath)
            ? filePath
            : null;
    }

    public MusicFileLocation LocateMusicFile(int fileId, out string filePath)
    {
        // todo implement
        filePath = null!;
        return MusicFileLocation.NotDownloaded;
    }
}