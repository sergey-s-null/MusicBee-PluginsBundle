using Mead.MusicBee.Enums;

namespace Module.MusicSourcesStorage.Core.Entities.Abstract;

public interface IModuleConfiguration
{
    string VkDocumentsDownloadingDirectory { get; }
    string SourceFilesDownloadingDirectory { get; }
    string DatabaseConnectionString { get; }
    int CoverSize { get; }

    /// <summary>
    /// MusicBee file field where should be located id of file from database.
    /// </summary>
    MetaDataType FileIdField { get; }
}