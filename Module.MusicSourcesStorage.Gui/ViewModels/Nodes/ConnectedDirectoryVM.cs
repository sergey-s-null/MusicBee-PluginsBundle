using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Module.Core.Helpers;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.Helpers;
using Module.MusicSourcesStorage.Logic.Enums;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using Module.Mvvm.Extension;
using Module.Mvvm.Extension.Helpers;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.Nodes;

[AddINotifyPropertyChangedInterface]
public sealed class ConnectedDirectoryVM : DirectoryVM, IConnectedDirectoryVM
{
    /// <summary>
    /// Depends on <see cref="INodeVM.ChildNodes"/> implementing <see cref="IProcessableVM"/>.<see cref="IProcessableVM.IsProcessing"/>
    /// </summary>
    public bool IsProcessing { get; private set; }

    /// <summary>
    /// Depends on <see cref="INodeVM.ChildNodes"/> implementing <see cref="IDownloadableVM"/>.<see cref="IDownloadableVM.CanDownload"/>
    /// </summary>
    public bool CanDownload { get; private set; }

    /// <summary>
    /// Depends on <see cref="INodeVM.ChildNodes"/> implementing <see cref="IDownloadableVM"/>.<see cref="IDownloadableVM.IsDownloaded"/>
    /// </summary>
    public bool IsDownloaded { get; private set; }

    /// <summary>
    /// Depends on <see cref="INodeVM.ChildNodes"/> implementing <see cref="IDeletableVM"/>.<see cref="IDeletableVM.CanDelete"/>
    /// </summary>
    public bool CanDelete { get; private set; }

    /// <summary>
    /// Depends on <see cref="INodeVM.ChildNodes"/> implementing <see cref="IDeletableVM"/>.<see cref="IDeletableVM.IsDeleted"/>
    /// </summary>
    public bool IsDeleted { get; private set; }

    /// <summary>
    /// Depends on <see cref="INodeVM.ChildNodes"/> implementing <see cref="IMarkableAsListenedVM"/>.<see cref="IMarkableAsListenedVM.IsListened"/>
    /// </summary>
    public bool IsListened { get; private set; }

    /// <summary>
    /// Depends on <see cref="INodeVM.ChildNodes"/> implementing:<br/>
    /// <list type="number">
    ///     <item><see cref="IConnectedDirectoryVM"/>.<see cref="IConnectedDirectoryVM.IsAllListened"/></item>
    ///     <item><see cref="IMarkableAsListenedVM"/>.<see cref="IMarkableAsListenedVM.IsListened"/></item>
    /// </list>
    /// </summary>
    public bool IsAllListened { get; private set; }

    /// <summary>
    /// Depends on <see cref="INodeVM.ChildNodes"/> implementing:<br/>
    /// <list type="number">
    ///     <item><see cref="IConnectedDirectoryVM"/>.<see cref="IConnectedDirectoryVM.IsAllNotListened"/></item>
    ///     <item><see cref="IMarkableAsListenedVM"/>.<see cref="IMarkableAsListenedVM.IsListened"/></item>
    /// </list>
    /// </summary>
    public bool IsAllNotListened { get; private set; }

    /// <summary>
    /// Depends on <see cref="INodeVM.ChildNodes"/> implementing:<br/>
    /// <list type="number">
    ///     <item><see cref="IConnectedMusicFileVM"/>.<see cref="IConnectedMusicFileVM.Location"/></item>
    ///     <item><see cref="IConnectedDirectoryVM"/>.<see cref="IConnectedDirectoryVM.HasDownloadedAndNotAttachedToLibraryFiles"/></item>
    ///     <item><see cref="IDownloadableVM"/>.<see cref="IDownloadableVM.IsDownloaded"/></item>
    /// </list>
    /// </summary>
    public bool HasDownloadedAndNotAttachedToLibraryFiles { get; private set; }

    public BitmapSource? Cover { get; private set; }

    #region Commands

    public ICommand Download => _downloadCmd ??= new RelayCommand(DownloadCmd);
    public ICommand Delete => _deleteCmd ??= new RelayCommand(DeleteCmd);
    public ICommand DeleteNoPrompt => _deleteNoPromptCmd ??= new RelayCommand(DeleteNoPromptCmd);
    public ICommand MarkAsListened => _markAsListenedCmd ??= new RelayCommand(MarkAsListenedCmd);
    public ICommand MarkAsNotListened => _markAsNotListenedCmd ??= new RelayCommand(MarkAsNotListenedCmd);

    private ICommand? _downloadCmd;
    private ICommand? _deleteCmd;
    private ICommand? _deleteNoPromptCmd;
    private ICommand? _markAsListenedCmd;
    private ICommand? _markAsNotListenedCmd;

    #endregion

    private readonly int _sourceId;
    private readonly string _path;
    private readonly Lazy<string> _unifiedPath;
    private readonly ICoverSelectionService _coverSelectionService;

    public ConnectedDirectoryVM(
        int sourceId,
        string path,
        IReadOnlyList<INodeVM> childNodes,
        ICoverSelectionService coverSelectionService)
        : base(Path.GetFileName(path), childNodes)
    {
        _sourceId = sourceId;
        _path = path;
        _unifiedPath = new Lazy<string>(() => PathHelper.UnifyDirectoryPath(_path));
        _coverSelectionService = coverSelectionService;

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

    #endregion

    private async void InitializeAsync()
    {
        InitializeCoverSelectionEventHandlers();
        RegisterChildNodesCallbacks();
        UpdateState();
        await InitializeCoverAsync();
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
                Path.GetDirectoryName(args.ImageFileRelativePath) ?? string.Empty
            );
            if (_unifiedPath.Value != coverChangedDirectoryUnifiedPath)
            {
                return;
            }

            Application.Current.Dispatcher.Invoke(
                () => Cover = BitmapSourceHelper.Create(args.ImageData)
            );
        };
        _coverSelectionService.CoverRemoved += (_, args) =>
        {
            if (args.SourceId != _sourceId
                || PathHelper.UnifyDirectoryPath(args.DirectoryPath) != _unifiedPath.Value)
            {
                return;
            }

            Application.Current.Dispatcher.Invoke(
                () => Cover = null
            );
        };
    }

    private async Task InitializeCoverAsync()
    {
        var cover = await _coverSelectionService.GetCoverAsync(_sourceId, _path);
        if (cover is not null)
        {
            Cover = BitmapSourceHelper.Create(cover);
        }
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

    #region State calculation

    private void RegisterChildNodesCallbacks()
    {
        foreach (var processable in ChildNodes.OfType<IProcessableVM>())
        {
            ViewModelHelper.RegisterPropertyChangedCallback(
                processable,
                x => x.IsProcessing,
                (_, _) => IsProcessing = CalculateIsProcessing()
            );
        }

        foreach (var downloadable in ChildNodes.OfType<IDownloadableVM>())
        {
            ViewModelHelper.RegisterPropertyChangedCallback(
                downloadable,
                x => x.CanDownload,
                (_, _) => CanDownload = CalculateCanDownload()
            );
            ViewModelHelper.RegisterPropertyChangedCallback(
                downloadable,
                x => x.IsDownloaded,
                (_, _) => IsDownloaded = CalculateIsDownloaded()
            );
        }

        foreach (var deletable in ChildNodes.OfType<IDeletableVM>())
        {
            ViewModelHelper.RegisterPropertyChangedCallback(
                deletable,
                x => x.CanDelete,
                (_, _) => CanDelete = CalculateCanDelete()
            );
            ViewModelHelper.RegisterPropertyChangedCallback(
                deletable,
                x => x.IsDeleted,
                (_, _) => IsDeleted = CalculateIsDeleted()
            );
        }

        foreach (var listenable in ChildNodes.OfType<IMarkableAsListenedVM>())
        {
            ViewModelHelper.RegisterPropertyChangedCallback(
                listenable,
                x => x.IsListened,
                (_, _) => IsListened = CalculateIsListened()
            );
        }

        foreach (var node in ChildNodes)
        {
            switch (node)
            {
                case IConnectedDirectoryVM directory:
                    ViewModelHelper.RegisterPropertyChangedCallback(
                        directory,
                        x => x.IsAllListened,
                        (_, _) => IsAllListened = CalculateIsAllListened()
                    );
                    break;
                case IMarkableAsListenedVM listenable:
                    ViewModelHelper.RegisterPropertyChangedCallback(
                        listenable,
                        x => x.IsListened,
                        (_, _) => IsAllListened = CalculateIsAllListened()
                    );
                    break;
            }
        }

        foreach (var node in ChildNodes)
        {
            switch (node)
            {
                case IConnectedDirectoryVM directory:
                    ViewModelHelper.RegisterPropertyChangedCallback(
                        directory,
                        x => x.IsAllNotListened,
                        (_, _) => IsAllNotListened = CalculateIsAllNotListened()
                    );
                    break;
                case IMarkableAsListenedVM listenable:
                    ViewModelHelper.RegisterPropertyChangedCallback(
                        listenable,
                        x => x.IsListened,
                        (_, _) => IsAllNotListened = CalculateIsAllNotListened()
                    );
                    break;
            }
        }

        foreach (var node in ChildNodes)
        {
            switch (node)
            {
                case IConnectedMusicFileVM musicFile:
                    ViewModelHelper.RegisterPropertyChangedCallback(
                        musicFile,
                        x => x.Location,
                        (_, _) => HasDownloadedAndNotAttachedToLibraryFiles =
                            CalculateHasDownloadedAndNotAttachedToLibraryFiles()
                    );
                    break;
                case IConnectedDirectoryVM directory:
                    ViewModelHelper.RegisterPropertyChangedCallback(
                        directory,
                        x => x.HasDownloadedAndNotAttachedToLibraryFiles,
                        (_, _) => HasDownloadedAndNotAttachedToLibraryFiles =
                            CalculateHasDownloadedAndNotAttachedToLibraryFiles()
                    );
                    break;
                case IDownloadableVM downloadable:
                    ViewModelHelper.RegisterPropertyChangedCallback(
                        downloadable,
                        x => x.IsDownloaded,
                        (_, _) => HasDownloadedAndNotAttachedToLibraryFiles =
                            CalculateHasDownloadedAndNotAttachedToLibraryFiles()
                    );
                    break;
            }
        }
    }

    private void UpdateState()
    {
        IsProcessing = CalculateIsProcessing();
        CanDownload = CalculateCanDownload();
        IsDownloaded = CalculateIsDownloaded();
        CanDelete = CalculateCanDelete();
        IsDeleted = CalculateIsDeleted();
        IsListened = CalculateIsListened();
        IsAllListened = CalculateIsAllListened();
        IsAllNotListened = CalculateIsAllNotListened();
        HasDownloadedAndNotAttachedToLibraryFiles = CalculateHasDownloadedAndNotAttachedToLibraryFiles();
    }

    private bool CalculateIsProcessing()
    {
        return ChildNodes
            .OfType<IProcessableVM>()
            .Any(x => x.IsProcessing);
    }

    private bool CalculateCanDownload()
    {
        return ChildNodes
            .OfType<IDownloadableVM>()
            .Any(x => x.CanDownload);
    }

    private bool CalculateIsDownloaded()
    {
        return ChildNodes
            .OfType<IDownloadableVM>()
            .All(x => x.IsDownloaded);
    }

    private bool CalculateCanDelete()
    {
        return ChildNodes
            .OfType<IDeletableVM>()
            .Any(x => x.CanDelete);
    }

    private bool CalculateIsDeleted()
    {
        return ChildNodes
            .OfType<IDeletableVM>()
            .All(x => x.IsDeleted);
    }

    private bool CalculateIsListened()
    {
        return ChildNodes
            .OfType<IMarkableAsListenedVM>()
            .All(x => x.IsListened);
    }

    private bool CalculateIsAllListened()
    {
        foreach (var node in ChildNodes)
        {
            switch (node)
            {
                case IConnectedDirectoryVM { IsAllListened: false }:
                    return false;
                case IMarkableAsListenedVM { IsListened: false }:
                    return false;
            }
        }

        return true;
    }

    private bool CalculateIsAllNotListened()
    {
        foreach (var node in ChildNodes)
        {
            switch (node)
            {
                case IConnectedDirectoryVM { IsAllNotListened: false }:
                    return false;
                case IMarkableAsListenedVM { IsListened: true }:
                    return false;
            }
        }

        return true;
    }

    private bool CalculateHasDownloadedAndNotAttachedToLibraryFiles()
    {
        foreach (var node in ChildNodes)
        {
            switch (node)
            {
                case IConnectedMusicFileVM musicFile:
                    switch (musicFile.Location)
                    {
                        case MusicFileLocation.Library:
                            continue;
                        case MusicFileLocation.Incoming:
                            return true;
                    }

                    break;
                case IConnectedDirectoryVM { HasDownloadedAndNotAttachedToLibraryFiles: true }:
                    return true;
                case IDownloadableVM { IsDownloaded: true }:
                    return true;
            }
        }

        return false;
    }

    #endregion
}