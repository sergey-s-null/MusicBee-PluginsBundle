using Module.Core.Helpers;
using Module.MusicSourcesStorage.Core.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class SourceFilesPathService : ISourceFilesPathService
{
    private readonly IModuleConfiguration _configuration;

    public SourceFilesPathService(IModuleConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetSourceFileTargetPath(MusicSourceAdditionalInfo sourceAdditionalInfo, SourceFile file)
    {
        return Path.Combine(
            _configuration.SourceFilesDownloadingDirectory,
            PathHelper.ReplaceInvalidCharacters(sourceAdditionalInfo.TargetFilesDirectory, "_"),
            file.Path
        );
    }
}