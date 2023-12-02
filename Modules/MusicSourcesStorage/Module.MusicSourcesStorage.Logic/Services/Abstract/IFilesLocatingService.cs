using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IFilesLocatingService
{
    Task<string?> LocateFileAsync(int fileId, CancellationToken token = default);

    /// <param name="fileId">File id.</param>
    /// <param name="filePath">Path to file if located. Undefined otherwise.</param>
    MusicFileLocation LocateMusicFile(int fileId, out string filePath);
}