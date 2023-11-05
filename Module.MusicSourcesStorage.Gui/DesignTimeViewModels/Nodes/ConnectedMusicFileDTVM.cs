using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.Nodes;

public sealed class ConnectedMusicFileDTVM : MusicFileDTVM, IConnectedMusicFileVM
{
    public bool CanDownload => State is MusicFileState.NotListened or MusicFileState.ListenedAndDeleted;
    public bool IsDownloaded => State is MusicFileState.InIncoming or MusicFileState.InLibrary;

    public ListenedState ListenedState => State switch
    {
        MusicFileState.NotListened => ListenedState.NotListened,
        _ => ListenedState.Listened
    };

    public bool CanDelete => State == MusicFileState.InIncoming;

    public MusicFileState State { get; }

    public ICommand Download => null!;
    public ICommand MarkAsListened => null!;
    public ICommand MarkAsNotListened => null!;
    public ICommand DeleteAndMarkAsListened => null!;
    public ICommand Delete => null!;

    public ConnectedMusicFileDTVM() : this("some/path/to/music.mp3", MusicFileState.InLibrary)
    {
    }

    public ConnectedMusicFileDTVM(string path, MusicFileState state) : base(path)
    {
        State = state;
    }
}