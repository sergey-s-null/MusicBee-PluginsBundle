using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.Nodes;

public sealed class ConnectedMusicFileDTVM : MusicFileDTVM, IConnectedMusicFileVM
{
    public bool IsProcessing { get; }

    public bool CanDownload => Location is MusicFileLocation.NotDownloaded;
    public bool IsDownloaded => Location is MusicFileLocation.Incoming or MusicFileLocation.Library;

    public bool CanDelete => Location is not MusicFileLocation.NotDownloaded;
    public bool IsDeleted => Location is MusicFileLocation.NotDownloaded;

    public MusicFileLocation Location { get; }
    public bool IsListened { get; }

    public ICommand Download => null!;
    public ICommand MarkAsListened => null!;
    public ICommand MarkAsNotListened => null!;
    public ICommand DeleteAndMarkAsListened => null!;
    public ICommand Delete => null!;

    public ConnectedMusicFileDTVM()
        : this(
            "some/path/to/music.mp3",
            MusicFileLocation.Library,
            false
        )
    {
    }

    public ConnectedMusicFileDTVM(
        string path,
        MusicFileLocation location,
        bool isListened,
        bool isProcessing = false)
        : base(path)
    {
        Location = location;
        IsListened = isListened;
        IsProcessing = isProcessing;
    }
}