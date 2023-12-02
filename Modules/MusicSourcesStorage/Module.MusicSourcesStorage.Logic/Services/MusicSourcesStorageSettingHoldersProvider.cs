using Mead.MusicBee.Enums;
using Module.MusicSourcesStorage.Core;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using Module.Settings.Database.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class MusicSourcesStorageSettingHoldersProvider
    : IMusicSourcesStorageSettingHoldersProvider
{
    public ISettingHolder<string> VkDocumentsDownloadingDirectory { get; }
    public ISettingHolder<string> SourceFilesDownloadingDirectory { get; }
    public ISettingHolder<int> CoverSize { get; }
    public ISettingHolder<MetaDataType> FileIdField { get; }

    public MusicSourcesStorageSettingHoldersProvider(
        IModuleConfiguration configuration,
        ISettingsRepository settingsRepository)
    {
        VkDocumentsDownloadingDirectory = new StringSettingHolder(
            configuration.SettingsArea,
            "vk-documents-downloading-directory",
            string.Empty,
            settingsRepository
        );
        SourceFilesDownloadingDirectory = new StringSettingHolder(
            configuration.SettingsArea,
            "source-files-downloading-directory",
            string.Empty,
            settingsRepository
        );
        CoverSize = new Int32SettingHolder(
            configuration.SettingsArea,
            "cover-size",
            100,
            settingsRepository
        );
        FileIdField = new EnumSettingHolder<MetaDataType>(
            configuration.SettingsArea,
            "file-id-field",
            MetaDataType.Custom16,
            settingsRepository
        );
    }
}