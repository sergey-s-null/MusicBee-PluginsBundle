using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.Nodes;

public sealed class ConnectedMusicFileDTVM : MusicFileDTVM, IConnectedMusicFileVM
{
    public MusicFileState State { get; }

    // ReSharper disable once UnusedMember.Global
    public ConnectedMusicFileDTVM() : this("some/path/to/music.mp3", MusicFileState.InLibrary)
    {
    }

    public ConnectedMusicFileDTVM(string path, MusicFileState state) : base(path)
    {
        State = state;
    }
}