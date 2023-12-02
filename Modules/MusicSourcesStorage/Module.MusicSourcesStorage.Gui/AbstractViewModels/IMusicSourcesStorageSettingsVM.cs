using Mead.MusicBee.Enums;
using Module.Settings.Gui.AbstractViewModels;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels;

public interface IMusicSourcesStorageSettingsVM : IBaseSettingsVM
{
    string VkDocumentsDownloadingDirectory { get; set; }
    string SourceFilesDownloadingDirectory { get; set; }

    string CoverSize { get; set; }
    string? CoverSizeError { get; }

    IReadOnlyList<MetaDataType> AvailableFieldsForFileId { get; }
    MetaDataType SelectedFileIdField { get; set; }
}