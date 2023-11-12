using System.IO;
using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Logic.Enums;
using Module.Mvvm.Extension;
using Module.Mvvm.Extension.Helpers;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.Nodes;

[AddINotifyPropertyChangedInterface]
public sealed class ConnectedDirectoryVM : DirectoryVM, IConnectedDirectoryVM
{
    public bool IsProcessing { get; private set; }

    public bool CanDownload { get; private set; }
    public bool IsDownloaded { get; private set; }

    public bool CanDelete { get; private set; }
    public bool IsDeleted { get; private set; }

    public bool IsListened { get; private set; }

    public bool HasDownloadedAndNotAttachedToLibraryFiles { get; private set; }

    // todo
    public Stream? CoverStream => null;

    #region Commands

    public ICommand Download => _downloadCmd ??= new RelayCommand(DownloadCmd);
    public ICommand Delete => _deleteCmd ??= new RelayCommand(DeleteCmd);
    public ICommand MarkAsListened => _markAsListenedCmd ??= new RelayCommand(MarkAsListenedCmd);
    public ICommand MarkAsNotListened => _markAsNotListenedCmd ??= new RelayCommand(MarkAsNotListenedCmd);

    private ICommand? _downloadCmd;
    private ICommand? _deleteCmd;
    private ICommand? _markAsListenedCmd;
    private ICommand? _markAsNotListenedCmd;

    #endregion

    public ConnectedDirectoryVM(string name, IReadOnlyList<INodeVM> childNodes) : base(name, childNodes)
    {
        RegisterCallbacks();
        UpdateState();
    }

    private void DownloadCmd()
    {
        throw new NotImplementedException();
    }

    private void DeleteCmd()
    {
        throw new NotImplementedException();
    }

    private void MarkAsListenedCmd()
    {
        throw new NotImplementedException();
    }

    private void MarkAsNotListenedCmd()
    {
        throw new NotImplementedException();
    }

    private void RegisterCallbacks()
    {
        foreach (var processable in ChildNodes.OfType<IProcessableVM>())
        {
            ViewModelHelper.RegisterPropertyChangedCallback(
                processable,
                x => x.IsProcessing,
                (_, _) => UpdateIsProcessing()
            );
        }

        foreach (var downloadable in ChildNodes.OfType<IDownloadableVM>())
        {
            ViewModelHelper.RegisterPropertyChangedCallback(
                downloadable,
                x => x.CanDownload,
                (_, _) => UpdateCanDownload()
            );
            ViewModelHelper.RegisterPropertyChangedCallback(
                downloadable,
                x => x.IsDownloaded,
                (_, _) => UpdateIsDownloaded()
            );
        }

        foreach (var deletable in ChildNodes.OfType<IDeletableVM>())
        {
            ViewModelHelper.RegisterPropertyChangedCallback(
                deletable,
                x => x.CanDelete,
                (_, _) => UpdateCanDelete()
            );
            ViewModelHelper.RegisterPropertyChangedCallback(
                deletable,
                x => x.IsDeleted,
                (_, _) => UpdateIsDeleted()
            );
        }

        foreach (var listenable in ChildNodes.OfType<IMarkableAsListenedVM>())
        {
            ViewModelHelper.RegisterPropertyChangedCallback(
                listenable,
                x => x.IsListened,
                (_, _) => UpdateIsListened()
            );
        }

        foreach (var node in ChildNodes)
        {
            switch (node)
            {
                case IConnectedMusicFileVM musicFile:
                    ViewModelHelper.RegisterPropertyChangedCallback(
                        musicFile,
                        x => x.Location,
                        (_, _) => UpdateHasDownloadedAndNotAttachedToLibraryFiles()
                    );
                    break;
                case IConnectedDirectoryVM directory:
                    ViewModelHelper.RegisterPropertyChangedCallback(
                        directory,
                        x => x.HasDownloadedAndNotAttachedToLibraryFiles,
                        (_, _) => UpdateHasDownloadedAndNotAttachedToLibraryFiles()
                    );
                    break;
                case IDownloadableVM downloadable:
                    ViewModelHelper.RegisterPropertyChangedCallback(
                        downloadable,
                        x => x.IsDownloaded,
                        (_, _) => UpdateHasDownloadedAndNotAttachedToLibraryFiles()
                    );
                    break;
            }
        }
    }

    private void UpdateState()
    {
        UpdateIsProcessing();
        UpdateCanDownload();
        UpdateIsDownloaded();
        UpdateCanDelete();
        UpdateIsDeleted();
        UpdateIsListened();
        UpdateHasDownloadedAndNotAttachedToLibraryFiles();
    }

    private void UpdateIsProcessing()
    {
        IsProcessing = ChildNodes
            .OfType<IProcessableVM>()
            .Any(x => x.IsProcessing);
    }

    private void UpdateCanDownload()
    {
        CanDownload = ChildNodes
            .OfType<IDownloadableVM>()
            .Any(x => x.CanDownload);
    }

    private void UpdateIsDownloaded()
    {
        IsDownloaded = ChildNodes
            .OfType<IDownloadableVM>()
            .All(x => x.IsDownloaded);
    }

    private void UpdateCanDelete()
    {
        CanDelete = ChildNodes
            .OfType<IDeletableVM>()
            .Any(x => x.CanDelete);
    }

    private void UpdateIsDeleted()
    {
        IsDeleted = ChildNodes
            .OfType<IDeletableVM>()
            .All(x => x.IsDeleted);
    }

    private void UpdateIsListened()
    {
        IsListened = ChildNodes
            .OfType<IMarkableAsListenedVM>()
            .All(x => x.IsListened);
    }

    private void UpdateHasDownloadedAndNotAttachedToLibraryFiles()
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
                            HasDownloadedAndNotAttachedToLibraryFiles = true;
                            return;
                    }

                    break;
                case IConnectedDirectoryVM { HasDownloadedAndNotAttachedToLibraryFiles: true }:
                    HasDownloadedAndNotAttachedToLibraryFiles = true;
                    return;
                case IDownloadableVM { IsDownloaded: true }:
                    HasDownloadedAndNotAttachedToLibraryFiles = true;
                    return;
            }
        }

        HasDownloadedAndNotAttachedToLibraryFiles = false;
    }
}