using Mead.MusicBee.Enums;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IMusicSourcesStorageSettingsAccessor
{
    string VkDocumentsDownloadingDirectory { get; }
    string SourceFilesDownloadingDirectory { get; }
    int CoverSize { get; }

    /// <summary>
    /// MusicBee file field where should be located id of file from database.
    /// </summary>
    MetaDataType FileIdField { get; }
}