using System.Windows;
using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Extensions;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using Module.Mvvm.Extension;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.Nodes;

[AddINotifyPropertyChangedInterface]
public sealed class ConnectedUnknownFileVM : UnknownFileVM, IConnectedUnknownFileVM
{
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

    private ICommand? _downloadCmd;
    private ICommand? _deleteCmd;

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
        : base(unknownFile.Path)
    {
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
            await task.Activated().Task;

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

            var result = MessageBox.Show(
                "Delete image file?\n" +
                $"\tId: {_unknownFile.Id}\n" +
                $"\tSource path: {_unknownFile.Path}",
                "?",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            await _filesDeletingService.DeleteAsync(_unknownFile.Id);
            IsDownloaded = false;
        }
        finally
        {
            IsProcessing = false;
            _lock.Release();
        }
    }
}