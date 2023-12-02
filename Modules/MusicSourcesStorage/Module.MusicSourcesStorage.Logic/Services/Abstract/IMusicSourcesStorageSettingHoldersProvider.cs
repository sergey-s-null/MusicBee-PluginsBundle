using Mead.MusicBee.Enums;
using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services.Abstract;

public interface IMusicSourcesStorageSettingHoldersProvider
{
    ISettingHolder<string> VkDocumentsDownloadingDirectory { get; }
    ISettingHolder<string> SourceFilesDownloadingDirectory { get; }
    ISettingHolder<int> CoverSize { get; }

    ISettingHolder<MetaDataType> FileIdField { get; }
}