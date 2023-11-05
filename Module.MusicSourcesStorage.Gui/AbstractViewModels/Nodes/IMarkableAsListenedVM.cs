using System.Windows.Input;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

public interface IMarkableAsListenedVM
{
    bool IsListened { get; }

    ICommand MarkAsListened { get; }
    ICommand MarkAsNotListened { get; }
}