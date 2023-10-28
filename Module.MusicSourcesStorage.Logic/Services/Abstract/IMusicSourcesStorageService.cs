using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IMusicSourcesStorageService
{
    Task AddMusicSourceAsync(
        MusicSource musicSource,
        CancellationToken token = default
    );

    Task<IReadOnlyList<MusicSource>> GetMusicSourcesAsync(CancellationToken token = default);
}