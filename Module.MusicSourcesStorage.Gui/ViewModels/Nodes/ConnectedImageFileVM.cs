using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Logic.Entities;
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

    #region MyRegion

    public ICommand Download => _downloadCmd ??= new RelayCommand(DownloadCmd);
    public ICommand Delete => _deleteCmd ??= new RelayCommand(DeleteCmd);
    public ICommand SelectAsCover => _selectAsCoverCmd ??= new RelayCommand(SelectAsCoverCmd);

    private ICommand? _downloadCmd;
    private ICommand? _deleteCmd;
    private ICommand? _selectAsCoverCmd;

    #endregion

    private readonly SemaphoreSlim _lock = new(1);

    private readonly int _fileId;
    private readonly IFilesLocatingService _filesLocatingService;
    private readonly IFilesDownloadingService _filesDownloadingService;

    public ConnectedImageFileVM(
        ImageFile imageFile,
        IFilesLocatingService filesLocatingService,
        IFilesDownloadingService filesDownloadingService)
        : base(imageFile.Path)
    {
        _fileId = imageFile.Id;
        _filesLocatingService = filesLocatingService;
        _filesDownloadingService = filesDownloadingService;

        // todo init
        IsCover = false;

        Initialize();
    }

    private async void Initialize()
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
            await task.Activated().Task;

            IsDownloaded = true;
        }
        finally
        {
            IsProcessing = false;
            _lock.Release();
        }
    }

    private void DeleteCmd()
    {
        throw new NotImplementedException();
    }

    private void SelectAsCoverCmd()
    {
        throw new NotImplementedException();
    }
}