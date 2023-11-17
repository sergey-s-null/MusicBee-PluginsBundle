using System.Windows;
using System.Windows.Input;
using Module.Core.Helpers;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.Helpers;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Args;
using Module.MusicSourcesStorage.Logic.Extensions;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using Module.Mvvm.Extension;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.Nodes;

[AddINotifyPropertyChangedInterface]
public sealed class ConnectedImageFileVM : ImageFileVM, IConnectedImageFileVM
{
    public bool IsProcessing { get; private set; }

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

    #region MyRegion

    public ICommand Download => _downloadCmd ??= new RelayCommand(DownloadCmd);
    public ICommand Delete => _deleteCmd ??= new RelayCommand(DeleteCmd);
    public ICommand DeleteNoPrompt => _deleteNoPromptCmd ??= new RelayCommand(DeleteNoPromptCmd);
    public ICommand SelectAsCover => _selectAsCoverCmd ??= new RelayCommand(SelectAsCoverCmd);
    public ICommand RemoveCover => _removeCoverCmd ??= new RelayCommand(RemoveCoverCmd);

    private ICommand? _downloadCmd;
    private ICommand? _deleteCmd;
    private ICommand? _deleteNoPromptCmd;
    private ICommand? _selectAsCoverCmd;
    private ICommand? _removeCoverCmd;

    #endregion

    private readonly SemaphoreSlim _lock = new(1);

    private readonly int _sourceId;
    private readonly int _fileId;
    private readonly string _filePath;
    private readonly Lazy<string> _parentDirectoryPath;
    private readonly Lazy<string> _parentDirectoryUnifiedPath;
    private readonly IFilesLocatingService _filesLocatingService;
    private readonly IFilesDownloadingService _filesDownloadingService;
    private readonly IFilesDeletingService _filesDeletingService;
    private readonly ICoverSelectionService _coverSelectionService;

    public ConnectedImageFileVM(
        ImageFile imageFile,
        IFilesLocatingService filesLocatingService,
        IFilesDownloadingService filesDownloadingService,
        IFilesDeletingService filesDeletingService,
        ICoverSelectionService coverSelectionService)
        : base(imageFile.Path)
    {
        _sourceId = imageFile.SourceId;
        _fileId = imageFile.Id;
        _filePath = imageFile.Path;
        _parentDirectoryPath = new Lazy<string>(
            () => System.IO.Path.GetDirectoryName(_filePath) ?? string.Empty
        );
        _parentDirectoryUnifiedPath = new Lazy<string>(() =>
            PathHelper.UnifyDirectoryPath(_parentDirectoryPath.Value)
        );
        _filesLocatingService = filesLocatingService;
        _filesDownloadingService = filesDownloadingService;
        _filesDeletingService = filesDeletingService;
        _coverSelectionService = coverSelectionService;

        IsCover = imageFile.IsCover;

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

            Application.Current.Dispatcher.Invoke(
                () => IsCover = args.ImageFileId == _fileId
            );
        };
        _coverSelectionService.CoverRemoved += (_, args) =>
        {
            if (args.SourceId != _sourceId)
            {
                return;
            }

            if (PathHelper.UnifyDirectoryPath(args.DirectoryPath) != _parentDirectoryUnifiedPath.Value)
            {
                return;
            }

            Application.Current.Dispatcher.Invoke(
                () => IsCover = false
            );
        };
    }

    private async Task InitializeDownloadedStateAsync()
    {
        await _lock.WaitAsync();
        IsProcessing = true;
        try
        {
            var filePath = await _filesLocatingService.LocateFileAsync(_fileId);
            IsDownloaded = filePath is not null;
        }
        finally
        {
            IsProcessing = false;
            _lock.Release();
        }
    }

    #region Commands Implementation

    private async void DownloadCmd()
    {
        if (!await _lock.WaitAsync(TimeSpan.Zero))
        {
            return;
        }

        try
        {
            if (!CanDownload)
            {
                return;
            }

            IsProcessing = true;

            var task = await _filesDownloadingService.CreateFileDownloadTaskAsync(_fileId);
            await task.Activated(new FileDownloadArgs(true)).Task;

            IsDownloaded = true;
        }
        finally
        {
            IsProcessing = false;
            _lock.Release();
        }
    }

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

            IsProcessing = true;

            if (MessageBoxHelper.AskForDeletion(_fileId, _filePath) != MessageBoxResult.Yes)
            {
                return;
            }

            await DeleteInternalAsync();
        }
        finally
        {
            IsProcessing = false;
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

            IsProcessing = true;

            await DeleteInternalAsync();
        }
        finally
        {
            IsProcessing = false;
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

            IsProcessing = true;

            var task = await _coverSelectionService.CreateCoverSelectionTaskAsync(_fileId);
            await task.Activated(new CoverSelectionArgs(true)).Task;

            IsCover = true;
        }
        finally
        {
            IsProcessing = false;
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

            IsProcessing = true;

            await _coverSelectionService.RemoveCoverAsync(
                _sourceId,
                _parentDirectoryPath.Value
            );
            IsCover = false;
        }
        finally
        {
            IsProcessing = false;
            _lock.Release();
        }
    }

    private async Task DeleteInternalAsync()
    {
        await _filesDeletingService.DeleteAsync(_fileId);
        IsDownloaded = false;
    }

    #endregion
}