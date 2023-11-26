using Module.Core.Helpers;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class SourceFilesPathService : ISourceFilesPathService
{
    private readonly IMusicSourcesStorageSettingsAccessor _settingsAccessor;

    public SourceFilesPathService(IMusicSourcesStorageSettingsAccessor settingsAccessor)
    {
        _settingsAccessor = settingsAccessor;
    }

    public string GetSourceFilesRootDirectory(MusicSourceAdditionalInfo additionalInfo)
    {
        return Path.Combine(
            _settingsAccessor.SourceFilesDownloadingDirectory,
            GetFixedTargetFilesDirectory(additionalInfo)
        );
    }

    public string GetSourceFileTargetPath(MusicSourceAdditionalInfo additionalInfo, SourceFile file)
    {
        return Path.Combine(
            _settingsAccessor.SourceFilesDownloadingDirectory,
            GetFixedTargetFilesDirectory(additionalInfo),
            file.Path
        );
    }

    private static string GetFixedTargetFilesDirectory(MusicSourceAdditionalInfo additionalInfo)
    {
        return PathHelper.ReplaceInvalidChars(additionalInfo.TargetFilesDirectory, "_");
    }
}