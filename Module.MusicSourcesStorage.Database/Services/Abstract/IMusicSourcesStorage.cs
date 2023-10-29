using Module.MusicSourcesStorage.Database.Models;

namespace Module.MusicSourcesStorage.Database.Services.Abstract;

public interface IMusicSourcesStorage
{
    Task AddAsync(MusicSourceModel musicSource, CancellationToken token = default);

    Task<IReadOnlyList<MusicSourceModel>> GetAllAsync(CancellationToken token = default);

    Task<MusicSourceAdditionalInfoModel?> GetAdditionalInfoAsync(int id, CancellationToken token = default);

    /// <exception cref="DatabaseException">
    /// Error on receive or update additional info.
    /// </exception>
    Task UpdateAdditionalInfo(int id, MusicSourceAdditionalInfoModel additionalInfo, CancellationToken token = default);
}