using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IMusicSourcesStorageService
{
    Task AddMusicSourceAsync(
        MusicSource musicSource,
        CancellationToken token = default
    );

    Task<IReadOnlyList<MusicSource>> GetMusicSourcesAsync(CancellationToken token = default);

    Task<MusicSourceAdditionalInfo?> GetAdditionalInfoByIdAsync(int id, CancellationToken token = default);

    Task UpdateAdditionalInfoAsync(int id, MusicSourceAdditionalInfo additionalInfo, CancellationToken token = default);
}