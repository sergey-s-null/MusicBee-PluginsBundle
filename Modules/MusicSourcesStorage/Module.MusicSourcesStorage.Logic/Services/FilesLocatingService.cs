using Mead.MusicBee.Api.Services.Abstract;
using Module.MusicSourcesStorage.Logic.Enums;
using Module.MusicSourcesStorage.Logic.Exceptions;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class FilesLocatingService : IFilesLocatingService
{
    private readonly IMusicSourcesStorageSettingsAccessor _settingsAccessor;
    private readonly ISourceFilesPathService _sourceFilesPathService;
    private readonly IMusicSourcesStorageService _musicSourcesStorageService;
    private readonly IMusicBeeApi _musicBeeApi;

    public FilesLocatingService(
        IMusicSourcesStorageSettingsAccessor settingsAccessor,
        ISourceFilesPathService sourceFilesPathService,
        IMusicSourcesStorageService musicSourcesStorageService,
        IMusicBeeApi musicBeeApi)
    {
        _settingsAccessor = settingsAccessor;
        _sourceFilesPathService = sourceFilesPathService;
        _musicSourcesStorageService = musicSourcesStorageService;
        _musicBeeApi = musicBeeApi;
    }

    public async Task<bool> IsFileExistsAsync(int fileId, CancellationToken token = default)
    {
        var filePath = await LocateFileAsync(fileId, token);
        return filePath is not null;
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
        if (TryFindInInbox(fileId, out filePath))
        {
            return MusicFileLocation.Incoming;
        }

        if (TryFindInLibrary(fileId, out filePath))
        {
            return MusicFileLocation.Library;
        }

        return MusicFileLocation.NotDownloaded;
    }

    private bool TryFindInInbox(int fileId, out string filePath)
    {
        // 4 - Inbox
        return TryFindMusicFile(fileId, 4, out filePath);
    }

    private bool TryFindInLibrary(int fileId, out string filePath)
    {
        // 1 - Library
        return TryFindMusicFile(fileId, 1, out filePath);
    }

    private bool TryFindMusicFile(int fileId, int sourceType, out string filePath)
    {
        var query =
            $"<Source Type=\"{sourceType}\">" +
            $"    <Conditions CombineMethod=\"All\">" +
            $"        <Condition Field=\"{_settingsAccessor.FileIdField}\" Comparison=\"Is\" Value=\"{fileId}\" />" +
            $"    </Conditions>" +
            $"</Source>";

        var result = _musicBeeApi.Library_QueryFilesEx(query, out var files);
        if (!result)
        {
            throw new FileLocatingException("Error on query files using MusicBee API.");
        }

        if (files is null || files.Length == 0)
        {
            filePath = null!;
            return false;
        }

        if (files.Length != 1)
        {
            throw new FileLocatingException($"Got multiple files for file id {fileId}");
        }

        filePath = files[0];
        return true;
    }
}