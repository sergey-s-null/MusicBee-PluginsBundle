using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels;

public interface IMusicFileVM : IReadOnlyMusicFileVM
{
    MusicFileState State { get; }
}