using Module.Core.Helpers;
using Module.MusicSourcesStorage.Logic.Delegates;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Args;
using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;
using Module.MusicSourcesStorage.Logic.Factories;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using Void = Module.MusicSourcesStorage.Logic.Entities.Void;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class SourceFilesRetargetingService : ISourceFilesRetargetingService
{
    private readonly IMusicSourcesStorageService _musicSourcesStorageService;
    private readonly ISourceFilesPathService _sourceFilesPathService;
    private readonly INewFileInitializationService _newFileInitializationService;

    public SourceFilesRetargetingService(
        IMusicSourcesStorageService musicSourcesStorageService,
        ISourceFilesPathService sourceFilesPathService,
        INewFileInitializationService newFileInitializationService)
    {
        _musicSourcesStorageService = musicSourcesStorageService;
        _sourceFilesPathService = sourceFilesPathService;
        _newFileInitializationService = newFileInitializationService;
    }

    public IActivableTask<FilesRetargetingArgs, Void> CreateRetargetingTask()
    {
        return ActivableTaskFactory.CreateWithoutResult<FilesRetargetingArgs>(Retarget);
    }

    private void Retarget(FilesRetargetingArgs args, RelativeProgressCallback progressCallback, CancellationToken token)
    {
        Retarget(args.SourceId, args.PreviousAdditionalInfo, args.CurrentAdditionalInfo, progressCallback, token);
    }

    private void Retarget(
        int sourceId,
        MusicSourceAdditionalInfo previousAdditionalInfo,
        MusicSourceAdditionalInfo currentAdditionalInfo,
        RelativeProgressCallback? progressCallback,
        CancellationToken token)
    {
        var files = _musicSourcesStorageService.ListSourceFilesBySourceIdAsync(sourceId, token).Result;

        progressCallback?.Invoke(0);
        for (var i = 0; i < files.Count; i++)
        {
            token.ThrowIfCancellationRequested();
            RetargetFile(previousAdditionalInfo, currentAdditionalInfo, files[i]);
            progressCallback?.Invoke((double)(i + 1) / (files.Count + 1));
        }

        var previousRoot = _sourceFilesPathService.GetSourceFilesRootDirectory(previousAdditionalInfo);
        DirectoryHelper.DeleteEmpty(previousRoot, true);
        progressCallback?.Invoke(1);
    }

    private void RetargetFile(
        MusicSourceAdditionalInfo previousAdditionalInfo,
        MusicSourceAdditionalInfo currentAdditionalInfo,
        SourceFile sourceFile)
    {
        var previousPath = _sourceFilesPathService.GetSourceFileTargetPath(previousAdditionalInfo, sourceFile);
        if (!File.Exists(previousPath))
        {
            return;
        }

        var currentPath = _sourceFilesPathService.GetSourceFileTargetPath(currentAdditionalInfo, sourceFile);
        if (PathHelper.UnifyFilePath(previousPath) == PathHelper.UnifyFilePath(currentPath))
        {
            return;
        }

        var currentDirectory = Path.GetDirectoryName(currentPath);
        if (currentDirectory is not null && !Directory.Exists(currentDirectory))
        {
            Directory.CreateDirectory(currentDirectory);
        }

        File.Move(previousPath, currentPath);
        _newFileInitializationService.InitializeNewFile(sourceFile.Id, currentPath);
    }
}