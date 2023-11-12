using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Enums;
using Module.MusicSourcesStorage.Logic.Extensions;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using Module.Mvvm.Extension;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.Nodes;

[AddINotifyPropertyChangedInterface]
public sealed class ConnectedMusicFileVM : MusicFileVM, IConnectedMusicFileVM
{
    public bool IsProcessing { get; private set; }

    [DependsOn(nameof(IsDownloaded), nameof(IsProcessing))]
    public bool CanDownload => !IsDownloaded && !IsProcessing;

    [DependsOn(nameof(Location))]
    public bool IsDownloaded => Location is MusicFileLocation.Incoming or MusicFileLocation.Library;

    [DependsOn(nameof(IsDeleted), nameof(IsProcessing))]
    public bool CanDelete => !IsDeleted && !IsProcessing;

    [DependsOn(nameof(IsDownloaded))] public bool IsDeleted => !IsDownloaded;

    public bool IsListened { get; private set; }

    public MusicFileLocation Location { get; private set; }

    #region Commands

    public ICommand Download =>
        _downloadCmd ??= new RelayCommand(DownloadCmd);

    public ICommand MarkAsListened =>
        _markAsListenedCmd ??= new RelayCommand(MarkAsListenedCmd);

    public ICommand MarkAsNotListened =>
        _markAsNotListenedCmd ??= new RelayCommand(MarkAsNotListenedCmd);

    public ICommand DeleteAndMarkAsListened =>
        _deleteAndMarkAsListenedCmd ??= new RelayCommand(DeleteAndMarkAsListenedCmd);

    public ICommand Delete =>
        _deleteCmd ??= new RelayCommand(DeleteCmd);

    private ICommand? _downloadCmd;
    private ICommand? _markAsListenedCmd;
    private ICommand? _markAsNotListenedCmd;
    private ICommand? _deleteAndMarkAsListenedCmd;
    private ICommand? _deleteCmd;

    #endregion

    private readonly SemaphoreSlim _lock = new(1);

    private readonly MusicFile _musicFile;
    private readonly IFilesLocatingService _filesLocatingService;
    private readonly IFilesDownloadingService _filesDownloadingService;
    private readonly IMusicSourcesStorageService _musicSourcesStorageService;
    private readonly IFilesDeletingService _filesDeletingService;

    public ConnectedMusicFileVM(
        MusicFile musicFile,
        IFilesLocatingService filesLocatingService,
        IFilesDownloadingService filesDownloadingService,
        IMusicSourcesStorageService musicSourcesStorageService,
        IFilesDeletingService filesDeletingService)
        : base(musicFile.Path)
    {
        _musicFile = musicFile;
        _filesLocatingService = filesLocatingService;
        _filesDownloadingService = filesDownloadingService;
        _musicSourcesStorageService = musicSourcesStorageService;
        _filesDeletingService = filesDeletingService;

        IsListened = musicFile.IsListened;

        Initialize();
    }

    private async void Initialize()
    {
        await _lock.WaitAsync();
        try
        {
            IsProcessing = true;
            Location = _filesLocatingService.LocateMusicFile(_musicFile.Id, out _);
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

            var task = await _filesDownloadingService.CreateFileDownloadTaskAsync(_musicFile.Id);
            await task.Activated().Task;

            Location = MusicFileLocation.Incoming;
        }
        finally
        {
            IsProcessing = false;
            _lock.Release();
        }
    }

    private async void MarkAsListenedCmd()
    {
        if (!await _lock.WaitAsync(TimeSpan.Zero))
        {
            return;
        }

        try
        {
            if (IsListened)
            {
                return;
            }

            IsProcessing = true;

            await _musicSourcesStorageService.SetMusicFileIsListenedAsync(_musicFile.Id, true);
            IsListened = true;
        }
        finally
        {
            IsProcessing = false;
            _lock.Release();
        }
    }

    private async void MarkAsNotListenedCmd()
    {
        if (!await _lock.WaitAsync(TimeSpan.Zero))
        {
            return;
        }

        try
        {
            if (!IsListened)
            {
                return;
            }

            IsProcessing = true;

            await _musicSourcesStorageService.SetMusicFileIsListenedAsync(_musicFile.Id, false);
            IsListened = false;
        }
        finally
        {
            IsProcessing = false;
            _lock.Release();
        }
    }

    private async void DeleteAndMarkAsListenedCmd()
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

            await _filesDeletingService.DeleteAsync(_musicFile.Id);
            Location = MusicFileLocation.NotDownloaded;

            if (!IsListened)
            {
                await _musicSourcesStorageService.SetMusicFileIsListenedAsync(_musicFile.Id, true);
                IsListened = true;
            }
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

            await _filesDeletingService.DeleteAsync(_musicFile.Id);
            Location = MusicFileLocation.NotDownloaded;
        }
        finally
        {
            IsProcessing = false;
            _lock.Release();
        }
    }
}