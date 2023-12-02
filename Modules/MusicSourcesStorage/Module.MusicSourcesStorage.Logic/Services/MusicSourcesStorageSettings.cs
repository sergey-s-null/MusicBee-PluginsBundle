using Mead.MusicBee.Enums;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class MusicSourcesStorageSettings : IMusicSourcesStorageSettings
{
    public string VkDocumentsDownloadingDirectory
    {
        get => _settingHoldersProvider.VkDocumentsDownloadingDirectory.Value;
        set => _settingHoldersProvider.VkDocumentsDownloadingDirectory.Value = value;
    }

    public string SourceFilesDownloadingDirectory
    {
        get => _settingHoldersProvider.SourceFilesDownloadingDirectory.Value;
        set => _settingHoldersProvider.SourceFilesDownloadingDirectory.Value = value;
    }

    public int CoverSize
    {
        get => _settingHoldersProvider.CoverSize.Value;
        set => _settingHoldersProvider.CoverSize.Value = value;
    }

    public MetaDataType FileIdField
    {
        get => _settingHoldersProvider.FileIdField.Value;
        set => _settingHoldersProvider.FileIdField.Value = value;
    }

    private readonly IMusicSourcesStorageSettingHoldersProvider _settingHoldersProvider;

    public MusicSourcesStorageSettings(IMusicSourcesStorageSettingHoldersProvider settingHoldersProvider)
    {
        _settingHoldersProvider = settingHoldersProvider;
    }
}