using Module.MusicSourcesStorage.Database.Models;

namespace Module.MusicSourcesStorage.Database.Services.Abstract;

public interface IMusicSourcesRepository
{
    /// <summary>
    /// Add music source to storage and return added model with valid ids.
    /// </summary>
    Task<MusicSourceModel> AddAsync(MusicSourceModel musicSource, CancellationToken token = default);

    Task DeleteAsync(int id, CancellationToken token = default);

    Task<MusicSourceModel> GetSourceByFileIdAsync(
        int fileId,
        bool includeFiles = false,
        CancellationToken token = default
    );

    Task<IReadOnlyList<MusicSourceModel>> GetAllAsync(CancellationToken token = default);

    Task<MusicSourceAdditionalInfoModel?> FindAdditionalInfoAsync(int id, CancellationToken token = default);

    Task<MusicSourceAdditionalInfoModel> GetAdditionalInfoAsync(int id, CancellationToken token = default);

    /// <returns>
    /// Updated model.
    /// </returns>
    /// <exception cref="DatabaseException">
    /// Error on receive or update additional info.
    /// </exception>
    Task<MusicSourceAdditionalInfoModel> UpdateAdditionalInfo(
        int id,
        MusicSourceAdditionalInfoModel additionalInfo,
        CancellationToken token = default
    );

    Task<FileModel> GetSourceFileAsync(int fileId, bool includeSource = false, CancellationToken token = default);

    Task<IReadOnlyList<FileModel>> ListSourceFilesBySourceIdAsync(
        int sourceId,
        bool includeSource = false,
        CancellationToken token = default
    );

    Task SetMusicFileIsListenedAsync(int musicFileId, bool isListened, CancellationToken token = default);

    Task SetCoverAsync(int imageFileId, byte[] imageData, CancellationToken token = default);

    Task RemoveCoverAsync(int imageFileId, CancellationToken token = default);
}