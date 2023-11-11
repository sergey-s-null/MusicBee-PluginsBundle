using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Logic.Enums;
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

    public ConnectedMusicFileVM(string path) : base(path)
    {
        // todo init
        IsProcessing = false;
        IsListened = false;
        Location = MusicFileLocation.NotDownloaded;
    }

    private void DownloadCmd()
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

    private void DeleteAndMarkAsListenedCmd()
    {
        throw new NotImplementedException();
    }

    private void DeleteCmd()
    {
        throw new NotImplementedException();
    }
}