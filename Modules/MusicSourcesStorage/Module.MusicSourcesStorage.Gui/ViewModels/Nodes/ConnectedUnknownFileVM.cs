using System.Windows;
using System.Windows.Input;
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
public sealed class ConnectedUnknownFileVM : FileBaseVM, IConnectedUnknownFileVM
{
    public int Id { get; }

    public override string Name { get; }
    public override string Path { get; }

    public bool IsProcessing { get; private set; }

    [DependsOn(nameof(IsDownloaded), nameof(IsProcessing))]
    public bool CanDownload => !IsDownloaded && !IsProcessing;

    public bool IsDownloaded { get; private set; }

    [DependsOn(nameof(IsDeleted), nameof(IsProcessing))]
    public bool CanDelete => !IsDeleted && !IsProcessing;

    [DependsOn(nameof(IsDownloaded))] public bool IsDeleted => !IsDownloaded;

    #region Commands

    public ICommand Download => _downloadCmd ??= new RelayCommand(DownloadCmd);
    public ICommand Delete => _deleteCmd ??= new RelayCommand(DeleteCmd);
    public ICommand DeleteNoPrompt => _deleteNoPromptCmd ??= new RelayCommand(DeleteNoPromptCmd);

    private ICommand? _downloadCmd;
    private ICommand? _deleteCmd;
    private ICommand? _deleteNoPromptCmd;

    #endregion

    private readonly SemaphoreSlim _lock = new(1);

    private readonly UnknownFile _unknownFile;
    private readonly IFilesLocatingService _filesLocatingService;
    private readonly IFilesDownloadingService _filesDownloadingService;
    private readonly IFilesDeletingService _filesDeletingService;

    public ConnectedUnknownFileVM(
        UnknownFile unknownFile,
        IFilesLocatingService filesLocatingService,
        IFilesDownloadingService filesDownloadingService,
        IFilesDeletingService filesDeletingService)
    {
        Id = unknownFile.Id;
        Name = System.IO.Path.GetFileName(unknownFile.Path);
        Path = unknownFile.Path;
        _unknownFile = unknownFile;
        _filesLocatingService = filesLocatingService;
        _filesDownloadingService = filesDownloadingService;
        _filesDeletingService = filesDeletingService;

        Initialize();
    }

    private async void Initialize()
    {
        await _lock.WaitAsync();
        IsProcessing = true;
        try
        {
            var filePath = await _filesLocatingService.LocateFileAsync(_unknownFile.Id);
            IsDownloaded = filePath is not null;
        }
        finally
        {
            IsProcessing = false;
            _lock.Release();
        }
    }

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

            var task = await _filesDownloadingService.CreateFileDownloadTaskAsync(_unknownFile.Id);
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

            if (MessageBoxHelper.AskForDeletion(_unknownFile) != MessageBoxResult.Yes)
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

    private async Task DeleteInternalAsync()
    {
        await _filesDeletingService.DeleteAsync(_unknownFile.Id);
        IsDownloaded = false;
    }
}