using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

public interface IMarkableAsListenedVM
{
    ListenedState ListenedState { get; }

    ICommand MarkAsListened { get; }
    ICommand MarkAsNotListened { get; }
}