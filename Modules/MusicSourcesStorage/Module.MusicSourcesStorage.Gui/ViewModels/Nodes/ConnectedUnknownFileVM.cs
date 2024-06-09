using System.Windows;
using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.Commands;
using Module.MusicSourcesStorage.Gui.Helpers;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using Module.Mvvm.Extension;
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
                                || _downloadCommand.IsProcessing;

    private bool IsProcessingInternal { get; set; }

    public bool CanDownload => !IsDownloaded && !IsProcessing;

    public bool IsDownloaded { get; private set; }

    public bool CanDelete => !IsDeleted && !IsProcessing;

    public bool IsDeleted => !IsDownloaded;

    #region Commands

    public ICommand Download => _downloadCommand;
    public ICommand Delete => _deleteCmd ??= new RelayCommand(DeleteCmd);
    public ICommand DeleteNoPrompt => _deleteNoPromptCmd ??= new RelayCommand(DeleteNoPromptCmd);

    private readonly DownloadFileCommand _downloadCommand;
    private ICommand? _deleteCmd;
    private ICommand? _deleteNoPromptCmd;

    #endregion

    private readonly SemaphoreSlim _lock = new(1);

    private readonly UnknownFile _unknownFile;

    private readonly IScopedComponentModelDependencyService<ConnectedUnknownFileVM> _dependencyService;
    private readonly IFilesLocatingService _filesLocatingService;
    private readonly IFilesDeletingService _filesDeletingService;

    public ConnectedUnknownFileVM(
        UnknownFile unknownFile,
        IComponentModelDependencyServiceFactory dependencyServiceFactory,
        IFilesLocatingService filesLocatingService,
        IFilesDeletingService filesDeletingService,
        DownloadFileCommand.Factory downloadFileCommandFactory)
    {
        Id = unknownFile.Id;
        Name = System.IO.Path.GetFileName(unknownFile.Path);
        Path = unknownFile.Path;
        _unknownFile = unknownFile;
        _dependencyService = dependencyServiceFactory.CreateScoped(this);
        _filesLocatingService = filesLocatingService;
        _filesDeletingService = filesDeletingService;

        _downloadCommand = downloadFileCommandFactory(unknownFile.Id);
        _downloadCommand.Downloaded += (_, _) => IsDownloaded = true;

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
        await _lock.WaitAsync();
        IsProcessingInternal = true;
        try
        {
            var filePath = await _filesLocatingService.LocateFileAsync(_unknownFile.Id);
            IsDownloaded = filePath is not null;
        }
        finally
        {
            IsProcessingInternal = false;
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

            IsProcessingInternal = true;

            if (MessageBoxHelper.AskForDeletion(_unknownFile) != MessageBoxResult.Yes)
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

    private async Task DeleteInternalAsync()
    {
        await _filesDeletingService.DeleteAsync(_unknownFile.Id);
        IsDownloaded = false;
    }
}