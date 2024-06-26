﻿using Module.MusicSourcesStorage.Logic.Entities;

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

    Task DeleteMusicSourceAsync(int id, CancellationToken token = default);

    Task<MusicSource> GetMusicSourceByFileIdAsync(int fileId, CancellationToken token = default);

    Task<IReadOnlyList<MusicSource>> GetMusicSourcesAsync(CancellationToken token = default);

    Task<MusicSourceAdditionalInfo?> FindAdditionalInfoByIdAsync(int id, CancellationToken token = default);

    Task<MusicSourceAdditionalInfo> GetAdditionalInfoByIdAsync(int id, CancellationToken token = default);

    /// <returns>
    /// Updated model.
    /// </returns>
    Task<MusicSourceAdditionalInfo> UpdateAdditionalInfoAsync(
        int id,
        MusicSourceAdditionalInfo additionalInfo,
        CancellationToken token = default
    );

    Task<SourceFile> GetSourceFileAsync(int id, CancellationToken token = default);

    Task<IReadOnlyList<SourceFile>> ListSourceFilesBySourceIdAsync(int sourceId, CancellationToken token = default);

    Task SetMusicFileIsListenedAsync(int musicFileId, bool isListened, CancellationToken token = default);

    Task<bool> IsSelectedAsCoverAsync(int fileId, CancellationToken token = default);

    /// <returns>Unselected images.</returns>
    Task<IReadOnlyList<ImageFile>> SelectAsCoverAsync(
        int imageFileId,
        byte[] imageData,
        CancellationToken token = default
    );

    Task<byte[]?> FindCoverAsync(int sourceId, string directoryRelativePath, CancellationToken token = default);

    Task RemoveCoverAsync(int fileId, CancellationToken token = default);
}