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
}