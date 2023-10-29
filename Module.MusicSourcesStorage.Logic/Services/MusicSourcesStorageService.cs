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

    public Task AddMusicSourceAsync(MusicSource musicSource, CancellationToken token)
    {
        var model = musicSource.ToDbModel();

        return _musicSourcesStorage.AddAsync(model, token);
    }

    public async Task<IReadOnlyList<MusicSource>> GetMusicSourcesAsync(CancellationToken token)
    {
        var models = await _musicSourcesStorage.GetAllAsync(token);

        return models
            .Select(x => x.ToLogicModel())
            .ToList();
    }
}