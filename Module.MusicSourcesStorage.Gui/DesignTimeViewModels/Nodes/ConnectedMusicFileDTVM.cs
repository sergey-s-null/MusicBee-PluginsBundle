using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.Nodes;

public sealed class ConnectedMusicFileDTVM : MusicFileDTVM, IConnectedMusicFileVM
{
    public bool CanDownload => Location is MusicFileLocation.NotDownloaded;
    public bool IsDownloaded => Location is MusicFileLocation.Incoming or MusicFileLocation.Library;

    // todo get from ctor
    public bool IsListened => false;

    public bool CanDelete => Location == MusicFileLocation.Incoming;

    public MusicFileLocation Location { get; }

    public ICommand Download => null!;
    public ICommand MarkAsListened => null!;
    public ICommand MarkAsNotListened => null!;
    public ICommand DeleteAndMarkAsListened => null!;
    public ICommand Delete => null!;

    public ConnectedMusicFileDTVM() : this("some/path/to/music.mp3", MusicFileLocation.Library)
    {
    }

    public ConnectedMusicFileDTVM(string path, MusicFileLocation location) : base(path)
    {
        Location = location;
    }
}