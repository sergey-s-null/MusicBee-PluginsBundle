using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.Commands;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using Module.Mvvm.Extension.Extensions;
using Module.Mvvm.Extension.Services.Abstract;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.Nodes;

[AddINotifyPropertyChangedInterface]
public sealed class ConnectedUnknownFileVM : FileBaseVM, IConnectedUnknownFileVM
{
    public int Id { get; }

    public override string Name { get; }
    public override string Path { get; }

    public bool IsProcessing => IsProcessingInternal
                                || _downloadCommand.IsProcessing
                                || _deleteCommand.IsProcessing
                                || _deleteNoPromptCommand.IsProcessing;

    private bool IsProcessingInternal { get; set; }

    public bool CanDownload => !IsDownloaded && !IsProcessing;

    public bool IsDownloaded { get; private set; }

    public bool CanDelete => !IsDeleted && !IsProcessing;

    public bool IsDeleted => !IsDownloaded;

    #region Commands

    public ICommand Download => _downloadCommand;
    public ICommand Delete => _deleteCommand;
    public ICommand DeleteNoPrompt => _deleteNoPromptCommand;

    private readonly DownloadFileCommand _downloadCommand;
    private readonly DeleteFileCommand _deleteCommand;
    private readonly DeleteFileCommand _deleteNoPromptCommand;

    #endregion

    private readonly UnknownFile _unknownFile;

    private readonly IScopedComponentModelDependencyService<ConnectedUnknownFileVM> _dependencyService;
    private readonly IFilesLocatingService _filesLocatingService;

    public ConnectedUnknownFileVM(
        UnknownFile unknownFile,
        IComponentModelDependencyServiceFactory dependencyServiceFactory,
        IFilesLocatingService filesLocatingService,
        DownloadFileCommand.Factory downloadFileCommandFactory,
        DeleteFileCommand.Factory deleteFileCommandFactory)
    {
        Id = unknownFile.Id;
        Name = System.IO.Path.GetFileName(unknownFile.Path);
        Path = unknownFile.Path;
        _unknownFile = unknownFile;
        _dependencyService = dependencyServiceFactory.CreateScoped(this);
        _filesLocatingService = filesLocatingService;

        _downloadCommand = downloadFileCommandFactory(unknownFile.Id);
        _deleteCommand = deleteFileCommandFactory(unknownFile.Id, unknownFile.Path, askBeforeDelete: true);
        _deleteNoPromptCommand = deleteFileCommandFactory(unknownFile.Id, unknownFile.Path, askBeforeDelete: false);

        _downloadCommand.Downloaded += (_, _) => IsDownloaded = true;
        _deleteCommand.Deleted += (_, _) => IsDownloaded = false;
        _deleteNoPromptCommand.Deleted += (_, _) => IsDownloaded = false;

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
        IsProcessingInternal = true;
        try
        {
            var filePath = await _filesLocatingService.LocateFileAsync(_unknownFile.Id);
            IsDownloaded = filePath is not null;
        }
        finally
        {
            IsProcessingInternal = false;
        }
    }
}