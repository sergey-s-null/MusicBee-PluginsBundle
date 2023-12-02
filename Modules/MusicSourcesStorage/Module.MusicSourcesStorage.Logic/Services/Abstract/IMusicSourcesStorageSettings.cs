using Mead.MusicBee.Enums;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IMusicSourcesStorageSettings : IMusicSourcesStorageSettingsAccessor
{
    new string VkDocumentsDownloadingDirectory { get; set; }
    new string SourceFilesDownloadingDirectory { get; set; }
    new int CoverSize { get; set; }

    /// <inheritdoc cref="IMusicSourcesStorageSettingsAccessor.FileIdField"/>
    new MetaDataType FileIdField { get; set; }
}