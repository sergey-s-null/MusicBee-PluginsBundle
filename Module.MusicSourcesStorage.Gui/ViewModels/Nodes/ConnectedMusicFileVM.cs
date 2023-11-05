using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.Mvvm.Extension;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.Nodes;

[AddINotifyPropertyChangedInterface]
public sealed class ConnectedMusicFileVM : MusicFileVM, IConnectedMusicFileVM
{
    public bool CanDownload => throw new NotImplementedException();
    public bool IsDownloaded => throw new NotImplementedException();

    public ListenedState ListenedState => throw new NotImplementedException();

    [DependsOn(nameof(State))]
    public bool CanDelete =>
        State == MusicFileState.InIncoming;

    public MusicFileState State { get; private set; }

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

    public ConnectedMusicFileVM(string path) : base(path)
    {
        // todo get from api
        State = MusicFileState.NotListened;
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