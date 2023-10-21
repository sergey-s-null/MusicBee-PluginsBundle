using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels;

public interface IConnectedMusicFileVM : IReadOnlyMusicFileVM
{
    MusicFileState State { get; }
}