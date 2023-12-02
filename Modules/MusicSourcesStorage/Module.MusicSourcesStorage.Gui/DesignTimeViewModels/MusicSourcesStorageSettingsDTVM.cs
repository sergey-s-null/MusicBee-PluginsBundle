using System.Windows.Input;
using Mead.MusicBee.Enums;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels;

public sealed class MusicSourcesStorageSettingsDTVM : IMusicSourcesStorageSettingsVM
{
    public bool Loaded => true;
    public string LoadingErrorMessage => string.Empty;

    public string VkDocumentsDownloadingDirectory { get; set; } = "path/to/VkDocuments";
    public string SourceFilesDownloadingDirectory { get; set; } = "path/to/source-files";

    public string CoverSize { get; set; } = "32s";
    public string CoverSizeError => "Invalid";

    public IReadOnlyList<MetaDataType> AvailableFieldsForFileId { get; } = new[]
    {
        MetaDataType.Custom1,
        MetaDataType.Custom2,
        MetaDataType.Custom3,
    };

    public MetaDataType SelectedFileIdField { get; set; } = MetaDataType.Custom3;

    public ICommand ReloadCmd => null!;

    public void Load()
    {
    }

    public bool Save()
    {
        return true;
    }
}