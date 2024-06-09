using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.Commands;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Enums;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using Module.Mvvm.Extension.Extensions;
using Module.Mvvm.Extension.Services.Abstract;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.Nodes;

[AddINotifyPropertyChangedInterface]
public sealed class ConnectedMusicFileVM : FileBaseVM, IConnectedMusicFileVM
{
    public int Id { get; }

    public override string Name { get; }
    public override string Path { get; }

    public bool IsProcessing => IsProcessingInternal
                                || _downloadCommand.IsProcessing
                                || _markAsListenedCommand.IsProcessing
                                || _markAsNotListenedCommand.IsProcessing
                                || _deleteAndMarkAsListenedCommand.IsProcessing
                                || _deleteCommand.IsProcessing
                                || _deleteNoPromptCommand.IsProcessing;

    private bool IsProcessingInternal { get; set; }

    public bool CanDownload => !IsDownloaded && !IsProcessing;

    public bool IsDownloaded => Location is MusicFileLocation.Incoming or MusicFileLocation.Library;

    public bool CanDelete => !IsDeleted && !IsProcessing;

    public bool IsDeleted => !IsDownloaded;

    public bool IsListened { get; private set; }

    public MusicFileLocation Location { get; private set; }

    #region Commands

    public ICommand Download => _downloadCommand;

    public ICommand MarkAsListened => _markAsListenedCommand;

    public ICommand MarkAsNotListened => _markAsNotListenedCommand;

    public ICommand DeleteAndMarkAsListened => _deleteAndMarkAsListenedCommand;

    public ICommand Delete => _deleteCommand;

    public ICommand DeleteNoPrompt => _deleteNoPromptCommand;

    private readonly DownloadFileCommand _downloadCommand;
    private readonly ChangeMusicListenedStateCommand _markAsListenedCommand;
    private readonly ChangeMusicListenedStateCommand _markAsNotListenedCommand;
    private readonly DeleteMusicFileAndMarkAsListenedCommand _deleteAndMarkAsListenedCommand;
    private readonly DeleteFileCommand _deleteCommand;
    private readonly DeleteFileCommand _deleteNoPromptCommand;

    #endregion

    private readonly SemaphoreSlim _lock = new(1);

    private readonly MusicFile _musicFile;
    private readonly IScopedComponentModelDependencyService<ConnectedMusicFileVM> _dependencyService;
    private readonly IFilesLocatingService _filesLocatingService;

    public ConnectedMusicFileVM(
        MusicFile musicFile,
        IComponentModelDependencyServiceFactory dependencyServiceFactory,
        IFilesLocatingService filesLocatingService,
        DownloadFileCommand.Factory downloadFileCommandFactory,
        ChangeMusicListenedStateCommand.Factory changeMusicListenedStateCommandFactory,
        DeleteMusicFileAndMarkAsListenedCommand.Factory deleteMusicFileAndMarkAsListenedCommandFactory,
        DeleteFileCommand.Factory deleteFileCommandFactory)
    {
        Id = musicFile.Id;
        Name = System.IO.Path.GetFileName(musicFile.Path);
        Path = musicFile.Path;
        _musicFile = musicFile;
        _dependencyService = dependencyServiceFactory.CreateScoped(this);
        _filesLocatingService = filesLocatingService;

        IsListened = musicFile.IsListened;

        _downloadCommand = downloadFileCommandFactory(musicFile.Id);
        _markAsListenedCommand = changeMusicListenedStateCommandFactory(musicFile.Id, isListened: true);
        _markAsNotListenedCommand = changeMusicListenedStateCommandFactory(musicFile.Id, isListened: false);
        _deleteAndMarkAsListenedCommand = deleteMusicFileAndMarkAsListenedCommandFactory(
            musicFile.Id,
            musicFile.Path,
            askBeforeDelete: true
        );
        _deleteCommand = deleteFileCommandFactory(musicFile.Id, musicFile.Path, askBeforeDelete: true);
        _deleteNoPromptCommand = deleteFileCommandFactory(musicFile.Id, musicFile.Path, askBeforeDelete: false);

        _downloadCommand.Downloaded += (_, _) => Location = MusicFileLocation.Incoming;
        _markAsListenedCommand.Changed += (_, _) => IsListened = true;
        _markAsNotListenedCommand.Changed += (_, _) => IsListened = false;
        _deleteAndMarkAsListenedCommand.DeletedAndMarkedAsListened += (_, _) =>
        {
            Location = MusicFileLocation.NotDownloaded;
            IsListened = true;
        };
        _deleteCommand.Deleted += (_, _) => Location = MusicFileLocation.NotDownloaded;
        _deleteNoPromptCommand.Deleted += (_, _) => Location = MusicFileLocation.NotDownloaded;

        RegisterDependencies();
        Initialize();
    }

    private void RegisterDependencies()
    {
        // IsProcessing
        _dependencyService.RegisterDependency(
            x => x.IsProcessing,
            x => x.IsProcessingInternal
        );
        _dependencyService.RegisterDependency(
            x => x.IsProcessing,
            _downloadCommand,
            x => x.IsProcessing
        );
        _dependencyService.RegisterDependency(
            x => x.IsProcessing,
            _markAsListenedCommand,
            x => x.IsProcessing
        );
        _dependencyService.RegisterDependency(
            x => x.IsProcessing,
            _markAsNotListenedCommand,
            x => x.IsProcessing
        );
        _dependencyService.RegisterDependency(
            x => x.IsProcessing,
            _deleteAndMarkAsListenedCommand,
            x => x.IsProcessing
        );
        _dependencyService.RegisterDependency(
            x => x.IsProcessing,
            _deleteCommand,
            x => x.IsProcessing
        );
        _dependencyService.RegisterDependency(
            x => x.IsProcessing,
            _deleteNoPromptCommand,
            x => x.IsProcessing
        );
        // CanDownload
        _dependencyService.RegisterDependency(
            x => x.CanDownload,
            x => x.IsDownloaded
        );
        _dependencyService.RegisterDependency(
            x => x.CanDownload,
            x => x.IsProcessing
        );
        // IsDownloaded
        _dependencyService.RegisterDependency(
            x => x.IsDownloaded,
            x => x.Location
        );
        // CanDelete
        _dependencyService.RegisterDependency(
            x => x.CanDelete,
            x => x.IsDeleted
        );
        _dependencyService.RegisterDependency(
            x => x.CanDelete,
            x => x.IsProcessing
        );
        // IsDeleted
        _dependencyService.RegisterDependency(
            x => x.IsDeleted,
            x => x.IsDownloaded
        );
    }

    private async void Initialize()
    {
        await _lock.WaitAsync();
        try
        {
            IsProcessingInternal = true;
            Location = _filesLocatingService.LocateMusicFile(_musicFile.Id, out _);
        }
        finally
        {
            IsProcessingInternal = false;
            _lock.Release();
        }
    }
}