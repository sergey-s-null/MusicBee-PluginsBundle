using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class FilesDeletingService : IFilesDeletingService
{
    private readonly IMusicSourcesStorageService _musicSourcesStorageService;
    private readonly ISourceFilesPathService _pathService;

    public FilesDeletingService(
        IMusicSourcesStorageService musicSourcesStorageService,
        ISourceFilesPathService pathService)
    {
        _musicSourcesStorageService = musicSourcesStorageService;
        _pathService = pathService;
    }

    public async Task DeleteAsync(int fileId, CancellationToken token = default)
    {
        var file = await _musicSourcesStorageService.GetSourceFileAsync(fileId, token);
        var additionalInfo = await _musicSourcesStorageService.GetAdditionalInfoByIdAsync(file.SourceId, token);

        var targetPath = _pathService.GetSourceFileTargetPath(additionalInfo, file);

        if (File.Exists(targetPath))
        {
            File.Delete(targetPath);
        }
    }
}