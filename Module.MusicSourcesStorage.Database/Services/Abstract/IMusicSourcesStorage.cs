using Module.MusicSourcesStorage.Database.Models;

namespace Module.MusicSourcesStorage.Database.Services.Abstract;

public interface IMusicSourcesStorage
{
    Task AddAsync(MusicSourceModel musicSource, CancellationToken token = default);

    Task<IReadOnlyList<MusicSourceModel>> GetAllAsync(CancellationToken token = default);

    Task<MusicSourceAdditionalInfoModel?> GetAdditionalInfoByIdAsync(int id, CancellationToken token = default);
}