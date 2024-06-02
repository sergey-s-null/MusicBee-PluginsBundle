using System.Windows;
using System.Windows.Input;
using Module.Core.Services.Abstract;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.Commands;
using Module.MusicSourcesStorage.Gui.Helpers;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Args;
using Module.MusicSourcesStorage.Logic.Extensions;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using Module.Mvvm.Extension;
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

    [DependsOn(nameof(IsProcessingInternal))]
    public bool IsProcessing => IsProcessingInternal || _downloadCmd.IsProcessing;

    [DependsOn(nameof(IsDownloaded), nameof(IsProcessing))]
    public bool CanDownload => !IsDownloaded && !IsProcessing;

    public bool IsDownloaded { get; private set; }

    [DependsOn(nameof(IsDeleted), nameof(IsProcessing))]
    public bool CanDelete => !IsDeleted && !IsProcessing;

    [DependsOn(nameof(IsDownloaded))] public bool IsDeleted => !IsDownloaded;

    public bool IsCover { get; private set; }

    [DependsOn(nameof(IsCover), nameof(IsProcessing))]
    public bool CanSelectAsCover => !IsCover && !IsProcessing;

    [DependsOn(nameof(IsCover), nameof(IsProcessing))]
    public bool CanRemoveCover => IsCover && !IsProcessing;

    private bool IsProcessingInternal { get; set; }

    #region Commands

    public ICommand Download => _downloadCmd;
    public ICommand Delete => _deleteCmd ??= new RelayCommand(DeleteCmd);
    public ICommand DeleteNoPrompt => _deleteNoPromptCmd ??= new RelayCommand(DeleteNoPromptCmd);
    public ICommand SelectAsCover => _selectAsCoverCmd ??= new RelayCommand(SelectAsCoverCmd);
    public ICommand RemoveCover => _removeCoverCmd ??= new RelayCommand(RemoveCoverCmd);

    private readonly DownloadFileCommand _downloadCmd;
    private ICommand? _deleteCmd;
    private ICommand? _deleteNoPromptCmd;
    private ICommand? _selectAsCoverCmd;
    private ICommand? _removeCoverCmd;

    #endregion

    private readonly SemaphoreSlim _lock = new(1);

    private readonly int _sourceId;
    private readonly string _filePath;
    private readonly IUiDispatcherProvider _dispatcherProvider;
    private readonly IFilesLocatingService _filesLocatingService;
    private readonly IFilesDeletingService _filesDeletingService;
    private readonly ICoverSelectionService _coverSelectionService;

    public ConnectedImageFileVM(
        ImageFile imageFile,
        IComponentModelDependencyService componentModelDependencyService,
        IUiDispatcherProvider dispatcherProvider,
        IFilesLocatingService filesLocatingService,
        IFilesDeletingService filesDeletingService,
        ICoverSelectionService coverSelectionService,
        DownloadFileCommand.Factory downloadFileCommandFactory)
    {
        Id = imageFile.Id;
        Name = System.IO.Path.GetFileName(imageFile.Path);
        Path = imageFile.Path;
        _sourceId = imageFile.SourceId;
        _filePath = imageFile.Path;
        _dispatcherProvider = dispatcherProvider;
        _filesLocatingService = filesLocatingService;
        _filesDeletingService = filesDeletingService;
        _coverSelectionService = coverSelectionService;

        IsCover = imageFile.IsCover;

        _downloadCmd = downloadFileCommandFactory(imageFile.Id);
        _downloadCmd.Downloaded += (_, _) => IsDownloaded = true;
        componentModelDependencyService.RegisterDependency(
            this,
            x => x.IsProcessing,
            _downloadCmd,
            x => x.IsProcessing
        );

        InitializeAsync();
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

    #region Commands Implementation

    private async void DeleteCmd()
    {
        if (!await _lock.WaitAsync(TimeSpan.Zero))
        {
            return;
        }

        try
        {
            if (!CanDelete)
            {
                return;
            }

            IsProcessingInternal = true;

            if (MessageBoxHelper.AskForDeletion(Id, _filePath) != MessageBoxResult.Yes)
            {
                return;
            }

            await DeleteInternalAsync();
        }
        finally
        {
            IsProcessingInternal = false;
            _lock.Release();
        }
    }

    private async void DeleteNoPromptCmd()
    {
        if (!await _lock.WaitAsync(TimeSpan.Zero))
        {
            return;
        }

        try
        {
            if (!CanDelete)
            {
                return;
            }

            IsProcessingInternal = true;

            await DeleteInternalAsync();
        }
        finally
        {
            IsProcessingInternal = false;
            _lock.Release();
        }
    }

    private async void SelectAsCoverCmd()
    {
        if (!await _lock.WaitAsync(TimeSpan.Zero))
        {
            return;
        }

        try
        {
            if (IsCover)
            {
                return;
            }

            IsProcessingInternal = true;

            var task = await _coverSelectionService.CreateCoverSelectionTaskAsync(Id);
            await task.Activated(new CoverSelectionArgs(true)).Task;

            IsCover = true;
            await UpdateDownloadedStateNotLockedAsync();
        }
        finally
        {
            IsProcessingInternal = false;
            _lock.Release();
        }
    }

    private async void RemoveCoverCmd()
    {
        if (!await _lock.WaitAsync(TimeSpan.Zero))
        {
            return;
        }

        try
        {
            if (!IsCover)
            {
                return;
            }

            IsProcessingInternal = true;

            await _coverSelectionService.RemoveCoverAsync(Id);
            IsCover = false;
        }
        finally
        {
            IsProcessingInternal = false;
            _lock.Release();
        }
    }

    private async Task DeleteInternalAsync()
    {
        await _filesDeletingService.DeleteAsync(Id);
        IsDownloaded = false;
    }

    #endregion

    private async Task UpdateDownloadedStateNotLockedAsync()
    {
        var filePath = await _filesLocatingService.LocateFileAsync(Id);
        IsDownloaded = filePath is not null;
    }
}