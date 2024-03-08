using Module.Core.Helpers;
using Module.MusicSourcesStorage.Database.Models;
using Module.MusicSourcesStorage.Database.Services.Abstract;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Extensions;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class MusicSourcesStorageService : IMusicSourcesStorageService
{
    private readonly IMusicSourcesRepository _musicSourcesRepository;

    public MusicSourcesStorageService(IMusicSourcesRepository musicSourcesRepository)
    {
        _musicSourcesRepository = musicSourcesRepository;
    }

    public async Task<MusicSource> AddMusicSourceAsync(MusicSource musicSource, CancellationToken token)
    {
        var model = musicSource.ToDbModel();

        model = await _musicSourcesRepository.AddAsync(model, token);

        return model.ToLogicModel();
    }

    public Task DeleteMusicSourceAsync(int id, CancellationToken token = default)
    {
        return _musicSourcesRepository.DeleteAsync(id, token);
    }

    public async Task<MusicSource> GetMusicSourceByFileIdAsync(int fileId, CancellationToken token)
    {
        var model = await _musicSourcesRepository.GetSourceByFileIdAsync(fileId, true, token);
        return model.ToLogicModel();
    }

    public async Task<IReadOnlyList<MusicSource>> GetMusicSourcesAsync(CancellationToken token)
    {
        var models = await _musicSourcesRepository.GetAllAsync(token);

        return models
            .Select(x => x.ToLogicModel())
            .ToList();
    }

    public async Task<MusicSourceAdditionalInfo?> FindAdditionalInfoByIdAsync(int id, CancellationToken token)
    {
        var model = await _musicSourcesRepository.FindAdditionalInfoAsync(id, token);
        return model?.ToLogicModel();
    }

    public async Task<MusicSourceAdditionalInfo> GetAdditionalInfoByIdAsync(int id, CancellationToken token)
    {
        var model = await _musicSourcesRepository.GetAdditionalInfoAsync(id, token);
        return model.ToLogicModel();
    }

    public async Task<MusicSourceAdditionalInfo> UpdateAdditionalInfoAsync(
        int id,
        MusicSourceAdditionalInfo additionalInfo,
        CancellationToken token)
    {
        var model = additionalInfo.ToDbModel();
        model = await _musicSourcesRepository.UpdateAdditionalInfo(id, model, token);
        return model.ToLogicModel();
    }

    public async Task<SourceFile> GetSourceFileAsync(int id, CancellationToken token)
    {
        var model = await _musicSourcesRepository.GetSourceFileAsync(id, false, token);
        return model.ToLogicModel();
    }

    public async Task<IReadOnlyList<SourceFile>> ListSourceFilesBySourceIdAsync(int sourceId, CancellationToken token)
    {
        var models = await _musicSourcesRepository.ListSourceFilesBySourceIdAsync(sourceId, false, token);
        return models
            .Select(x => x.ToLogicModel())
            .ToList();
    }

    public Task SetMusicFileIsListenedAsync(int musicFileId, bool isListened, CancellationToken token)
    {
        return _musicSourcesRepository.SetMusicFileIsListenedAsync(musicFileId, isListened, token);
    }

    public async Task<bool> IsSelectedAsCoverAsync(int fileId, CancellationToken token)
    {
        var file = await _musicSourcesRepository.GetSourceFileAsync(fileId, includeSource: false, token);
        return file is ImageFileModel { IsCover: true };
    }
    
    public async Task SelectAsCoverAsync(int imageFileId, byte[] imageData, CancellationToken token)
    {
        var source = await _musicSourcesRepository.GetSourceByFileIdAsync(imageFileId, false, token);

        var sourceFiles = await _musicSourcesRepository.ListSourceFilesBySourceIdAsync(source.Id, false, token);
        var selectedAsCoverFiles = sourceFiles
            .OfType<ImageFileModel>()
            .Where(x => x.IsCover)
            .ToList();
        foreach (var selectedAsCoverFile in selectedAsCoverFiles)
        {
            await _musicSourcesRepository.RemoveCoverAsync(selectedAsCoverFile.Id, token);
        }

        await _musicSourcesRepository.SetCoverAsync(imageFileId, imageData, token);
    }

    public async Task<byte[]?> FindCoverAsync(
        int sourceId,
        string directoryRelativePath,
        CancellationToken token)
    {
        var files = await _musicSourcesRepository.ListSourceFilesBySourceIdAsync(sourceId, false, token);
        var directoryUnifiedPath = PathHelper.UnifyDirectoryPath(directoryRelativePath);
        var cover = files
            .OfType<ImageFileModel>()
            .Where(x => IsFileInDirectory(x, directoryUnifiedPath))
            .FirstOrDefault(x => x.IsCover);

        return cover?.Data;
    }

    public async Task RemoveCoverAsync(int sourceId, string directoryRelativePath, CancellationToken token)
    {
        var files = await _musicSourcesRepository.ListSourceFilesBySourceIdAsync(sourceId, false, token);

        var directoryUnifiedPath = PathHelper.UnifyDirectoryPath(directoryRelativePath);
        var coversToRemove = files
            .OfType<ImageFileModel>()
            .Where(x => x.IsCover)
            .Where(x => IsFileInDirectory(x, directoryUnifiedPath))
            .ToList();

        foreach (var cover in coversToRemove)
        {
            await _musicSourcesRepository.RemoveCoverAsync(cover.Id, token);
        }
    }

    private static bool IsFileInDirectory(FileModel file, string directoryUnifiedPath)
    {
        var fileDirectory = Path.GetDirectoryName(file.Path) ?? string.Empty;
        return PathHelper.UnifyDirectoryPath(fileDirectory) == directoryUnifiedPath;
    }
}