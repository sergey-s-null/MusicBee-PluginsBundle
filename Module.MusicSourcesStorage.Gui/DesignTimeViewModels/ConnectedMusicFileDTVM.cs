using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels;

public sealed class ConnectedMusicFileDTVM : IConnectedMusicFileVM
{
    public string Name { get; }
    public string Path { get; }
    public MusicFileState State { get; }
    public IReadOnlyList<INodeVM> ChildNodes { get; } = Array.Empty<INodeVM>();

    // ReSharper disable once UnusedMember.Global
    public ConnectedMusicFileDTVM() : this("some/path/to/music.mp3", MusicFileState.InLibrary)
    {
    }

    public ConnectedMusicFileDTVM(string path, MusicFileState state)
    {
        Name = System.IO.Path.GetFileName(path);
        Path = path;
        State = state;
    }
}