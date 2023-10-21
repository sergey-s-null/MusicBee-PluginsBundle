using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels;

public interface IConnectedMusicFileVM : IMusicFileVM
{
    MusicFileState State { get; }
}