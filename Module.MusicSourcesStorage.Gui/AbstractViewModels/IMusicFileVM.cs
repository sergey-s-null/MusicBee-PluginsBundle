using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels;

public interface IMusicFileVM : IFileVM
{
    MusicFileState State { get; }
}