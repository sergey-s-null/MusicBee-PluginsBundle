using Module.Core.Helpers;
using Module.MusicSourcesStorage.Database.Models;
using Module.MusicSourcesStorage.Database.Services.Abstract;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Extensions;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class MusicSourcesStorageService : IMusicSourcesStorageService
{
    private readonly IMusicSourcesStorage _musicSourcesStorage;

    public MusicSourcesStorageService(IMusicSourcesStorage musicSourcesStorage)
    {
        _musicSourcesStorage = musicSourcesStorage;
    }

    public async Task<MusicSource> AddMusicSourceAsync(MusicSource musicSource, CancellationToken token)
    {
        var model = musicSource.ToDbModel();

        model = await _musicSourcesStorage.AddAsync(model, token);

        return model.ToLogicModel();
    }

    public Task DeleteMusicSourceAsync(int id, CancellationToken token = default)
    {
        return _musicSourcesStorage.DeleteAsync(id, token);
    }

    public async Task<MusicSource> GetMusicSourceByFileIdAsync(int fileId, CancellationToken token)
    {
        var model = await _musicSourcesStorage.GetSourceByFileIdAsync(fileId, true, token);
        return model.ToLogicModel();
    }

    public async Task<IReadOnlyList<MusicSource>> GetMusicSourcesAsync(CancellationToken token)
    {
        var models = await _musicSourcesStorage.GetAllAsync(token);

        return models
            .Select(x => x.ToLogicModel())
            .ToList();
    }

    public async Task<MusicSourceAdditionalInfo?> FindAdditionalInfoByIdAsync(int id, CancellationToken token)
    {
        var model = await _musicSourcesStorage.FindAdditionalInfoAsync(id, token);
        return model?.ToLogicModel();
    }

    public async Task<MusicSourceAdditionalInfo> GetAdditionalInfoByIdAsync(int id, CancellationToken token)
    {
        var model = await _musicSourcesStorage.GetAdditionalInfoAsync(id, token);
        return model.ToLogicModel();
    }

    public async Task<MusicSourceAdditionalInfo> UpdateAdditionalInfoAsync(
        int id,
        MusicSourceAdditionalInfo additionalInfo,
        CancellationToken token)
    {
        var model = additionalInfo.ToDbModel();
        model = await _musicSourcesStorage.UpdateAdditionalInfo(id, model, token);
        return model.ToLogicModel();
    }

    public async Task<SourceFile> GetSourceFileAsync(int id, CancellationToken token)
    {
        var model = await _musicSourcesStorage.GetSourceFileAsync(id, false, token);
        return model.ToLogicModel();
    }

    public async Task<IReadOnlyList<SourceFile>> ListSourceFilesBySourceIdAsync(int sourceId, CancellationToken token)
    {
        var models = await _musicSourcesStorage.ListSourceFilesBySourceIdAsync(sourceId, false, token);
        return models
            .Select(x => x.ToLogicModel())
            .ToList();
    }

    public Task SetMusicFileIsListenedAsync(int musicFileId, bool isListened, CancellationToken token)
    {
        return _musicSourcesStorage.SetMusicFileIsListenedAsync(musicFileId, isListened, token);
    }

    public async Task SelectAsCoverAsync(int imageFileId, byte[] imageData, CancellationToken token)
    {
        var source = await _musicSourcesStorage.GetSourceByFileIdAsync(imageFileId, false, token);

        var sourceFiles = await _musicSourcesStorage.ListSourceFilesBySourceIdAsync(source.Id, false, token);
        var selectedAsCoverFiles = sourceFiles
            .OfType<ImageFileModel>()
            .Where(x => x.IsCover)
            .ToList();
        foreach (var selectedAsCoverFile in selectedAsCoverFiles)
        {
            await _musicSourcesStorage.RemoveCoverAsync(selectedAsCoverFile.Id, token);
        }

        await _musicSourcesStorage.SetCoverAsync(imageFileId, imageData, token);
    }

    public async Task<byte[]?> FindCoverAsync(
        int sourceId,
        string directoryRelativePath,
        CancellationToken token)
    {
        var files = await _musicSourcesStorage.ListSourceFilesBySourceIdAsync(sourceId, false, token);
        var directoryUnifiedPath = PathHelper.UnifyDirectoryPath(directoryRelativePath);
        var cover = files
            .OfType<ImageFileModel>()
            .Where(x => IsFileInDirectory(x, directoryUnifiedPath))
            .FirstOrDefault(x => x.IsCover);

        return cover?.Data;
    }

    public async Task RemoveCoverAsync(int sourceId, string directoryRelativePath, CancellationToken token)
    {
        var files = await _musicSourcesStorage.ListSourceFilesBySourceIdAsync(sourceId, false, token);

        var directoryUnifiedPath = PathHelper.UnifyDirectoryPath(directoryRelativePath);
        var coversToRemove = files
            .OfType<ImageFileModel>()
            .Where(x => x.IsCover)
            .Where(x => IsFileInDirectory(x, directoryUnifiedPath))
            .ToList();

        foreach (var cover in coversToRemove)
        {
            await _musicSourcesStorage.RemoveCoverAsync(cover.Id, token);
        }
    }

    private static bool IsFileInDirectory(FileModel file, string directoryUnifiedPath)
    {
        var fileDirectory = Path.GetDirectoryName(file.Path) ?? string.Empty;
        return PathHelper.UnifyDirectoryPath(fileDirectory) == directoryUnifiedPath;
    }
}