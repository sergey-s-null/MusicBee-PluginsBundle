using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Module.Core.Helpers;
using Module.Core.Services.Abstract;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.Helpers;
using Module.MusicSourcesStorage.Logic.Enums;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using Module.Mvvm.Extension;
using Module.Mvvm.Extension.Extensions;
using Module.Mvvm.Extension.Services.Abstract;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.Nodes;

[AddINotifyPropertyChangedInterface]
public sealed class ConnectedDirectoryVM : IConnectedDirectoryVM
{
    #region INodeVM properties

    public string Name { get; }
    public string Path { get; }

    public bool IsExpanded
    {
        get => _isExpanded;
        set
        {
            if (value)
            {
                LoadChildNodes();
            }
            else
            {
                UnloadChildNodes();
            }

            _isExpanded = value;
        }
    }

    private bool _isExpanded;

    public IReadOnlyList<INodeVM> ChildNodes => _childNodesCollection;
    private readonly ObservableCollection<INodeVM> _childNodesCollection = new() { null! }; // todo use placeholder?

    #endregion

    #region IProcessableVM properties

    public bool IsProcessing => Calculate_IsProcessing();

    #endregion

    #region IDownloadableVM properties

    public bool CanDownload => Calculate_CanDownload();

    public bool IsDownloaded => Calculate_IsDownloaded();

    #endregion

    #region IDeletableVM properties

    public bool CanDelete => Calculate_CanDelete();

    public bool IsDeleted => Calculate_IsDeleted();

    #endregion

    #region IMarkableAsListenedVM properties

    public bool IsListened => Calculate_IsListened();

    #endregion

    #region ICoverRemovableVM properties

    public bool CanRemoveCover => Cover is not null && !IsProcessing;

    #endregion

    #region IConnectedDirectoryVM properties

    public bool IsAllListened => Calculate_IsAllListened();

    public bool IsAllNotListened => Calculate_IsAllNotListened();

    public bool HasDownloadedAndNotAttachedToLibraryFiles =>
        Calculate_HasDownloadedAndNotAttachedToLibraryFiles();

    public BitmapSource? Cover { get; private set; }

    #endregion

    #region Commands

    public ICommand Download => _downloadCmd ??= new RelayCommand(DownloadCmd);
    public ICommand Delete => _deleteCmd ??= new RelayCommand(DeleteCmd);
    public ICommand DeleteNoPrompt => _deleteNoPromptCmd ??= new RelayCommand(DeleteNoPromptCmd);
    public ICommand MarkAsListened => _markAsListenedCmd ??= new RelayCommand(MarkAsListenedCmd);
    public ICommand MarkAsNotListened => _markAsNotListenedCmd ??= new RelayCommand(MarkAsNotListenedCmd);
    public ICommand RemoveCover => _removeCoverCmd ??= new RelayCommand(RemoveCoverCmd);

    private ICommand? _downloadCmd;
    private ICommand? _deleteCmd;
    private ICommand? _deleteNoPromptCmd;
    private ICommand? _markAsListenedCmd;
    private ICommand? _markAsNotListenedCmd;
    private ICommand? _removeCoverCmd;

    #endregion

    private readonly IScopedComponentModelDependencyService<ConnectedDirectoryVM> _dependencyService;
    private readonly IUiDispatcherProvider _dispatcherProvider;
    private readonly ICoverSelectionService _coverSelectionService;

    private bool _isChildNodesLoaded;

    private readonly int _sourceId;
    private readonly Lazy<string> _unifiedPath;
    private readonly Func<IReadOnlyList<INodeVM>> _childNodesFactory;
    private readonly Lazy<IReadOnlyList<INodeVM>> _childNodes;

    public delegate ConnectedDirectoryVM Factory(
        int sourceId,
        string path,
        Func<IReadOnlyList<INodeVM>> childNodesFactory
    );

    public ConnectedDirectoryVM(
        int sourceId,
        string path,
        Func<IReadOnlyList<INodeVM>> childNodesFactory,
        IComponentModelDependencyServiceFactory dependencyServiceFactory,
        IUiDispatcherProvider dispatcherProvider,
        ICoverSelectionService coverSelectionService)
    {
        _dependencyService = dependencyServiceFactory.CreateScoped(this);
        _dispatcherProvider = dispatcherProvider;
        _coverSelectionService = coverSelectionService;

        Path = path;
        Name = System.IO.Path.GetFileName(path);

        _sourceId = sourceId;
        _unifiedPath = new Lazy<string>(() => PathHelper.UnifyDirectoryPath(path));
        _childNodesFactory = childNodesFactory;
        _childNodes = new Lazy<IReadOnlyList<INodeVM>>(CreateChildNodesAndRegisterDependencies);

        InitializeAsync();
    }

    #region Commands implementation

    private void DownloadCmd()
    {
        if (!CanDownload)
        {
            return;
        }

        var nodesCanBeDownloaded = ChildNodes
            .OfType<IDownloadableVM>()
            .Where(x => x.CanDownload);

        foreach (var node in nodesCanBeDownloaded)
        {
            node.Download.Execute(null);
        }
    }

    private void DeleteCmd()
    {
        if (!CanDelete)
        {
            return;
        }

        var result = MessageBox.Show(
            $"Delete all files under folder \"{Name}\"?",
            "\u255a(•\u2302•)\u255d",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question
        );

        if (result != MessageBoxResult.Yes)
        {
            return;
        }

        CallDeleteNoPromptOnChildNodes();
    }

    private void DeleteNoPromptCmd()
    {
        if (!CanDelete)
        {
            return;
        }

        CallDeleteNoPromptOnChildNodes();
    }

    private void MarkAsListenedCmd()
    {
        if (IsAllListened)
        {
            return;
        }

        foreach (var listenable in ChildNodes.OfType<IMarkableAsListenedVM>())
        {
            listenable.MarkAsListened.Execute(null);
        }
    }

    private void MarkAsNotListenedCmd()
    {
        if (IsAllNotListened)
        {
            return;
        }

        foreach (var listenable in ChildNodes.OfType<IMarkableAsListenedVM>())
        {
            listenable.MarkAsNotListened.Execute(null);
        }
    }

    private void RemoveCoverCmd()
    {
        var images = _childNodes.Value
            .OfType<IConnectedImageFileVM>()
            .Where(x => x.CanRemoveCover)
            .ToList();

        foreach (var image in images)
        {
            image.RemoveCover.Execute(null);
        }
    }

    #endregion

    private IReadOnlyList<INodeVM> CreateChildNodesAndRegisterDependencies()
    {
        var childNodes = _childNodesFactory();
        RegisterDependenciesOnChildNodes(childNodes);
        return childNodes;
    }

    private void RegisterDependenciesOnChildNodes(IReadOnlyList<INodeVM> childNodes)
    {
        RegisterDependencies_CanDownload(childNodes);
        RegisterDependencies_IsDownloaded(childNodes);
        RegisterDependencies_CanDelete(childNodes);
        RegisterDependencies_IsDeleted(childNodes);
        RegisterDependencies_IsListened(childNodes);
        RegisterDependencies_IsAllListened(childNodes);
        RegisterDependencies_IsAllNotListened(childNodes);
        RegisterDependencies_HasDownloadedAndNotAttachedToLibraryFiles(childNodes);
        RegisterDependencies_IsProcessing(childNodes);
    }

    private async void InitializeAsync()
    {
        RegisterDependencies();
        InitializeCoverSelectionEventHandlers();
        await InitializeCoverAsync();
    }

    private void RegisterDependencies()
    {
        _dependencyService.RegisterDependency(
            x => x.CanRemoveCover,
            x => x.Cover
        );
        _dependencyService.RegisterDependency(
            x => x.CanRemoveCover,
            x => x.IsProcessing
        );
    }

    private void InitializeCoverSelectionEventHandlers()
    {
        _coverSelectionService.CoverChanged += (_, args) =>
        {
            if (args.SourceId != _sourceId)
            {
                return;
            }

            var coverChangedDirectoryUnifiedPath = PathHelper.UnifyDirectoryPath(
                System.IO.Path.GetDirectoryName(args.ImageFileRelativePath) ?? string.Empty
            );
            if (_unifiedPath.Value != coverChangedDirectoryUnifiedPath)
            {
                return;
            }

            _dispatcherProvider.Dispatcher.Invoke(
                () => Cover = BitmapSourceHelper.Create(args.ImageData)
            );
        };
        _coverSelectionService.CoverRemoved += (_, args) =>
        {
            var isCoverRemovedUnderCurrentDir = _childNodes.Value
                .OfType<IConnectedFileVM>()
                .Any(x => x.Id == args.FileId);
            if (isCoverRemovedUnderCurrentDir)
            {
                _dispatcherProvider.Dispatcher.Invoke(() => Cover = null);
            }
        };
    }

    private async Task InitializeCoverAsync()
    {
        var cover = await _coverSelectionService.GetCoverAsync(_sourceId, Path);
        if (cover is not null)
        {
            Cover = BitmapSourceHelper.Create(cover);
        }
    }

    private void LoadChildNodes()
    {
        if (_isChildNodesLoaded)
        {
            return;
        }

        _childNodesCollection.Clear();
        foreach (var childNode in _childNodes.Value)
        {
            _childNodesCollection.Add(childNode);
        }

        _isChildNodesLoaded = true;
    }

    private void UnloadChildNodes()
    {
        if (!_isChildNodesLoaded)
        {
            return;
        }

        _childNodesCollection.Clear();
        _childNodesCollection.Add(null!);

        _isChildNodesLoaded = false;
    }

    private void CallDeleteNoPromptOnChildNodes()
    {
        var nodesCanBeDeleted = ChildNodes
            .OfType<IDeletableVM>()
            .Where(x => x.CanDelete);

        foreach (var node in nodesCanBeDeleted)
        {
            node.DeleteNoPrompt.Execute(null);
        }
    }

    #region Properties calculation and dependencies

    private bool Calculate_CanDownload()
    {
        return _childNodes.Value
            .OfType<IDownloadableVM>()
            .Any(x => x.CanDownload);
    }

    private void RegisterDependencies_CanDownload(IReadOnlyList<INodeVM> childNodes)
    {
        foreach (var downloadable in childNodes.OfType<IDownloadableVM>())
        {
            _dependencyService.RegisterDependency(
                x => x.CanDownload,
                downloadable,
                x => x.CanDownload
            );
        }
    }

    private bool Calculate_IsDownloaded()
    {
        return _childNodes.Value
            .OfType<IDownloadableVM>()
            .All(x => x.IsDownloaded);
    }

    private void RegisterDependencies_IsDownloaded(IReadOnlyList<INodeVM> childNodes)
    {
        foreach (var downloadable in childNodes.OfType<IDownloadableVM>())
        {
            _dependencyService.RegisterDependency(
                x => x.IsDownloaded,
                downloadable,
                x => x.IsDownloaded
            );
        }
    }

    private bool Calculate_CanDelete()
    {
        return _childNodes.Value
            .OfType<IDeletableVM>()
            .Any(x => x.CanDelete);
    }

    private void RegisterDependencies_CanDelete(IReadOnlyList<INodeVM> childNodes)
    {
        foreach (var deletable in childNodes.OfType<IDeletableVM>())
        {
            _dependencyService.RegisterDependency(
                x => x.CanDelete,
                deletable,
                x => x.CanDelete
            );
        }
    }

    private bool Calculate_IsDeleted()
    {
        return _childNodes.Value
            .OfType<IDeletableVM>()
            .All(x => x.IsDeleted);
    }

    private void RegisterDependencies_IsDeleted(IReadOnlyList<INodeVM> childNodes)
    {
        foreach (var deletable in childNodes.OfType<IDeletableVM>())
        {
            _dependencyService.RegisterDependency(
                x => x.IsDeleted,
                deletable,
                x => x.IsDeleted
            );
        }
    }

    private bool Calculate_IsListened()
    {
        return _childNodes.Value
            .OfType<IMarkableAsListenedVM>()
            .All(x => x.IsListened);
    }

    private void RegisterDependencies_IsListened(IReadOnlyList<INodeVM> childNodes)
    {
        foreach (var markableAsListened in childNodes.OfType<IMarkableAsListenedVM>())
        {
            _dependencyService.RegisterDependency(
                x => x.IsListened,
                markableAsListened,
                x => x.IsListened
            );
        }
    }

    private bool Calculate_IsAllListened()
    {
        return _childNodes.Value
                   .OfType<IMarkableAsListenedVM>()
                   .All(x => x.IsListened)
               && _childNodes.Value
                   .OfType<IConnectedDirectoryVM>()
                   .All(x => x.IsAllListened);
    }

    private void RegisterDependencies_IsAllListened(IReadOnlyList<INodeVM> childNodes)
    {
        foreach (var markableAsListened in childNodes.OfType<IMarkableAsListenedVM>())
        {
            _dependencyService.RegisterDependency(
                x => x.IsAllListened,
                markableAsListened,
                x => x.IsListened
            );
        }

        foreach (var directory in childNodes.OfType<IConnectedDirectoryVM>())
        {
            _dependencyService.RegisterDependency(
                x => x.IsAllListened,
                directory,
                x => x.IsAllListened
            );
        }
    }

    private bool Calculate_IsAllNotListened()
    {
        return _childNodes.Value
                   .OfType<IMarkableAsListenedVM>()
                   .All(x => !x.IsListened)
               && _childNodes.Value
                   .OfType<IConnectedDirectoryVM>()
                   .All(x => x.IsAllNotListened);
    }

    private void RegisterDependencies_IsAllNotListened(IReadOnlyList<INodeVM> childNodes)
    {
        foreach (var markableAsListened in childNodes.OfType<IMarkableAsListenedVM>())
        {
            _dependencyService.RegisterDependency(
                x => x.IsAllNotListened,
                markableAsListened,
                x => x.IsListened
            );
        }

        foreach (var directory in childNodes.OfType<IConnectedDirectoryVM>())
        {
            _dependencyService.RegisterDependency(
                x => x.IsAllNotListened,
                directory,
                x => x.IsAllNotListened
            );
        }
    }

    private bool Calculate_HasDownloadedAndNotAttachedToLibraryFiles()
    {
        return _childNodes.Value
                   .OfType<IConnectedMusicFileVM>()
                   .Any(x => x.Location == MusicFileLocation.Incoming)
               || _childNodes.Value
                   .OfType<IDownloadableVM>()
                   .Where(x => x is not IConnectedDirectoryVM or IConnectedMusicFileVM)
                   .Any(x => x.IsDownloaded)
               || _childNodes.Value
                   .OfType<IConnectedDirectoryVM>()
                   .Any(x => x.HasDownloadedAndNotAttachedToLibraryFiles);
    }

    private void RegisterDependencies_HasDownloadedAndNotAttachedToLibraryFiles(IReadOnlyList<INodeVM> childNodes)
    {
        foreach (var musicFile in childNodes.OfType<IConnectedMusicFileVM>())
        {
            _dependencyService.RegisterDependency(
                x => x.HasDownloadedAndNotAttachedToLibraryFiles,
                musicFile,
                x => x.Location
            );
        }

        foreach (var downloadable in childNodes.OfType<IDownloadableVM>())
        {
            if (downloadable is IConnectedDirectoryVM or IConnectedMusicFileVM)
            {
                continue;
            }

            _dependencyService.RegisterDependency(
                x => x.HasDownloadedAndNotAttachedToLibraryFiles,
                downloadable,
                x => x.IsDownloaded
            );
        }

        foreach (var directory in childNodes.OfType<IConnectedDirectoryVM>())
        {
            _dependencyService.RegisterDependency(
                x => x.HasDownloadedAndNotAttachedToLibraryFiles,
                directory,
                x => x.HasDownloadedAndNotAttachedToLibraryFiles
            );
        }
    }

    private bool Calculate_IsProcessing()
    {
        return _childNodes.Value
            .OfType<IProcessableVM>()
            .Any(x => x.IsProcessing);
    }

    private void RegisterDependencies_IsProcessing(IReadOnlyList<INodeVM> childNodes)
    {
        foreach (var processable in childNodes.OfType<IProcessableVM>())
        {
            _dependencyService.RegisterDependency(
                x => x.IsProcessing,
                processable,
                x => x.IsProcessing
            );
        }
    }

    #endregion
}