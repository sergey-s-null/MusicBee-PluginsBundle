using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IMusicSourcesStorageService
{
    /// <summary>
    /// Add music source and return model with valid ids.
    /// </summary>
    Task<MusicSource> AddMusicSourceAsync(
        MusicSource musicSource,
        CancellationToken token = default
    );

    Task<IReadOnlyList<MusicSource>> GetMusicSourcesAsync(CancellationToken token = default);

    Task<MusicSourceAdditionalInfo?> GetAdditionalInfoByIdAsync(int id, CancellationToken token = default);

    Task UpdateAdditionalInfoAsync(int id, MusicSourceAdditionalInfo additionalInfo, CancellationToken token = default);
}