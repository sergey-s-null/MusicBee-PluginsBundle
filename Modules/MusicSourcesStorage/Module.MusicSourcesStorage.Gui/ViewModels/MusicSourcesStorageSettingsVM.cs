using System.Windows.Input;
using Mead.MusicBee.Enums;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using Module.Mvvm.Extension;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels;

[AddINotifyPropertyChangedInterface]
public sealed class MusicSourcesStorageSettingsVM : IMusicSourcesStorageSettingsVM
{
    private const int MinCoverSize = 10;
    private const int MaxCoverSize = 1000;

    public bool Loaded { get; private set; }
    public string LoadingErrorMessage => string.Empty;

    public string VkDocumentsDownloadingDirectory { get; set; } = string.Empty;
    public string SourceFilesDownloadingDirectory { get; set; } = string.Empty;

    [OnChangedMethod(nameof(OnCoverSizeChanged))]
    public string CoverSize { get; set; } = MinCoverSize.ToString();

    public string? CoverSizeError { get; private set; }

    public IReadOnlyList<MetaDataType> AvailableFieldsForFileId { get; } = new[]
    {
        MetaDataType.Custom1,
        MetaDataType.Custom2,
        MetaDataType.Custom3,
        MetaDataType.Custom4,
        MetaDataType.Custom5,
        MetaDataType.Custom6,
        MetaDataType.Custom7,
        MetaDataType.Custom8,
        MetaDataType.Custom9,
        MetaDataType.Custom10,
        MetaDataType.Custom11,
        MetaDataType.Custom12,
        MetaDataType.Custom13,
        MetaDataType.Custom14,
        MetaDataType.Custom15,
        MetaDataType.Custom16,
    };

    public MetaDataType SelectedFileIdField { get; set; }

    public ICommand ReloadCmd => _reloadCmd ??= new RelayCommand(Load);

    private ICommand? _reloadCmd;

    private readonly IMusicSourcesStorageBufferedSettings _bufferedSettings;

    public MusicSourcesStorageSettingsVM(IMusicSourcesStorageBufferedSettings bufferedSettings)
    {
        _bufferedSettings = bufferedSettings;
    }

    public void Load()
    {
        _bufferedSettings.Restore();

        VkDocumentsDownloadingDirectory = _bufferedSettings.VkDocumentsDownloadingDirectory;
        SourceFilesDownloadingDirectory = _bufferedSettings.SourceFilesDownloadingDirectory;
        CoverSize = _bufferedSettings.CoverSize.ToString();
        SelectedFileIdField = _bufferedSettings.FileIdField;

        Loaded = true;
    }

    public bool Save()
    {
        _bufferedSettings.VkDocumentsDownloadingDirectory = VkDocumentsDownloadingDirectory;
        _bufferedSettings.SourceFilesDownloadingDirectory = SourceFilesDownloadingDirectory;
        if (int.TryParse(CoverSize, out var coverSizeInt) && IsValidCoverSize(coverSizeInt))
        {
            _bufferedSettings.CoverSize = coverSizeInt;
        }

        _bufferedSettings.FileIdField = SelectedFileIdField;

        _bufferedSettings.Save();

        return true;
    }

    private void OnCoverSizeChanged()
    {
        if (!int.TryParse(CoverSize, out var coverSizeInt))
        {
            CoverSizeError = "Could not parse cover size.";
        }
        else if (!IsValidCoverSize(coverSizeInt))
        {
            CoverSizeError = $"Size should be in range [{MinCoverSize}; {MaxCoverSize}].";
        }
        else
        {
            CoverSizeError = null;
        }
    }

    private static bool IsValidCoverSize(int coverSize)
    {
        return coverSize is >= MinCoverSize and <= MaxCoverSize;
    }
}