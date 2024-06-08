using System.Windows.Input;
using Module.Core.Services.Abstract;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.Commands;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using Module.Mvvm.Extension.Extensions;
using Module.Mvvm.Extension.Services.Abstract;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.Nodes;

[AddINotifyPropertyChangedInterface]
public sealed class ConnectedImageFileVM : FileBaseVM, IConnectedImageFileVM
{
    public int Id { get; }

    public override string Name { get; }
    public override string Path { get; }

    public bool IsProcessing => IsProcessingInternal
                                || _downloadCmd.IsProcessing
                                || _deleteCmd.IsProcessing
                                || _deleteNoPromptCmd.IsProcessing
                                || _selectAsCoverCmd.IsProcessing
                                || _removeCoverCmd.IsProcessing;

    public bool CanDownload => !IsDownloaded && !IsProcessing;

    public bool IsDownloaded { get; private set; }

    public bool CanDelete => !IsDeleted && !IsProcessing;

    public bool IsDeleted => !IsDownloaded;

    public bool IsCover { get; private set; }

    public bool CanSelectAsCover => !IsCover && !IsProcessing;

    public bool CanRemoveCover => IsCover && !IsProcessing;

    private bool IsProcessingInternal { get; set; }

    #region Commands

    public ICommand Download => _downloadCmd;
    public ICommand Delete => _deleteCmd;
    public ICommand DeleteNoPrompt => _deleteNoPromptCmd;
    public ICommand SelectAsCover => _selectAsCoverCmd;
    public ICommand RemoveCover => _removeCoverCmd;

    private readonly DownloadFileCommand _downloadCmd;
    private readonly DeleteFileCommand _deleteCmd;
    private readonly DeleteFileCommand _deleteNoPromptCmd;
    private readonly SelectAsCoverCommand _selectAsCoverCmd;
    private readonly RemoveCoverCommand _removeCoverCmd;

    #endregion

    private readonly SemaphoreSlim _lock = new(1);

    private readonly int _sourceId;
    private readonly IUiDispatcherProvider _dispatcherProvider;
    private readonly IFilesLocatingService _filesLocatingService;
    private readonly ICoverSelectionService _coverSelectionService;

    public ConnectedImageFileVM(
        ImageFile imageFile,
        IComponentModelDependencyServiceFactory componentModelDependencyServiceFactory,
        IUiDispatcherProvider dispatcherProvider,
        IFilesLocatingService filesLocatingService,
        ICoverSelectionService coverSelectionService,
        DownloadFileCommand.Factory downloadFileCommandFactory,
        DeleteFileCommand.Factory deleteFileCommandFactory,
        SelectAsCoverCommand.Factory selectAsCoverCommandFactory,
        RemoveCoverCommand.Factory removeCoverCommandFactory)
    {
        Id = imageFile.Id;
        Name = System.IO.Path.GetFileName(imageFile.Path);
        Path = imageFile.Path;
        _sourceId = imageFile.SourceId;
        _dispatcherProvider = dispatcherProvider;
        _filesLocatingService = filesLocatingService;
        _coverSelectionService = coverSelectionService;

        IsCover = imageFile.IsCover;

        _downloadCmd = downloadFileCommandFactory(imageFile.Id);
        _deleteCmd = deleteFileCommandFactory(imageFile.Id, imageFile.Path, askBeforeDelete: true);
        _deleteNoPromptCmd = deleteFileCommandFactory(imageFile.Id, imageFile.Path, askBeforeDelete: false);
        _selectAsCoverCmd = selectAsCoverCommandFactory(imageFile.Id);
        _removeCoverCmd = removeCoverCommandFactory(imageFile.Id);

        RegisterCommandHandlers();

        var dependencyService = componentModelDependencyServiceFactory.CreateScoped(this);
        RegisterDependencies(dependencyService);

        InitializeAsync();
    }

    private void RegisterCommandHandlers()
    {
        _downloadCmd.Downloaded += (_, _) => _dispatcherProvider.Dispatcher.Invoke(
            () => IsDownloaded = true
        );
        _deleteCmd.Deleted += (_, _) => _dispatcherProvider.Dispatcher.Invoke(
            () => IsDownloaded = false
        );
        _deleteNoPromptCmd.Deleted += (_, _) => _dispatcherProvider.Dispatcher.Invoke(
            () => IsDownloaded = false
        );
        _selectAsCoverCmd.Selected += async (_, _) => await _dispatcherProvider.Dispatcher.Invoke(
            async () =>
            {
                IsCover = true;
                await UpdateDownloadedStateNotLockedAsync();
            }
        );
    }

    private void RegisterDependencies(
        IScopedComponentModelDependencyService<ConnectedImageFileVM> dependencyService)
    {
        #region IsProcessing

        dependencyService.RegisterDependency(
            x => x.IsProcessing,
            x => x.IsProcessingInternal
        );
        dependencyService.RegisterDependency(
            x => x.IsProcessing,
            _downloadCmd,
            x => x.IsProcessing
        );
        dependencyService.RegisterDependency(
            x => x.IsProcessing,
            _deleteCmd,
            x => x.IsProcessing
        );
        dependencyService.RegisterDependency(
            x => x.IsProcessing,
            _deleteNoPromptCmd,
            x => x.IsProcessing
        );
        dependencyService.RegisterDependency(
            x => x.IsProcessing,
            _selectAsCoverCmd,
            x => x.IsProcessing
        );
        dependencyService.RegisterDependency(
            x => x.IsProcessing,
            _removeCoverCmd,
            x => x.IsProcessing
        );

        #endregion

        #region CanDownload

        dependencyService.RegisterDependency(
            x => x.CanDownload,
            x => x.IsDownloaded
        );
        dependencyService.RegisterDependency(
            x => x.CanDownload,
            x => x.IsProcessing
        );

        #endregion

        #region CanDelete

        dependencyService.RegisterDependency(
            x => x.CanDelete,
            x => x.IsDeleted
        );
        dependencyService.RegisterDependency(
            x => x.CanDelete,
            x => x.IsProcessing
        );

        #endregion

        #region IsDeleted

        dependencyService.RegisterDependency(
            x => x.IsDeleted,
            x => x.IsDownloaded
        );

        #endregion

        #region CanSelectAsCover

        dependencyService.RegisterDependency(
            x => x.CanSelectAsCover,
            x => x.IsCover
        );
        dependencyService.RegisterDependency(
            x => x.CanSelectAsCover,
            x => x.IsProcessing
        );

        #endregion

        #region CanRemoveCover

        dependencyService.RegisterDependency(
            x => x.CanRemoveCover,
            x => x.IsCover
        );
        dependencyService.RegisterDependency(
            x => x.CanRemoveCover,
            x => x.IsProcessing
        );

        #endregion
    }

    private async void InitializeAsync()
    {
        InitializeCoverSelectionEventsHandlers();
        await InitializeDownloadedStateAsync();
    }

    private void InitializeCoverSelectionEventsHandlers()
    {
        _coverSelectionService.CoverChanged += (_, args) =>
        {
            if (args.SourceId != _sourceId)
            {
                return;
            }

            _dispatcherProvider.Dispatcher.Invoke(
                () => IsCover = args.ImageFileId == Id
            );
        };
        _coverSelectionService.CoverRemoved += (_, args) =>
        {
            if (args.FileId == Id)
            {
                _dispatcherProvider.Dispatcher.Invoke(() => IsCover = false);
            }
        };
    }

    private async Task InitializeDownloadedStateAsync()
    {
        await _lock.WaitAsync();
        IsProcessingInternal = true;
        try
        {
            await UpdateDownloadedStateNotLockedAsync();
        }
        finally
        {
            IsProcessingInternal = false;
            _lock.Release();
        }
    }

    private async Task UpdateDownloadedStateNotLockedAsync()
    {
        var filePath = await _filesLocatingService.LocateFileAsync(Id);
        IsDownloaded = filePath is not null;
    }
}