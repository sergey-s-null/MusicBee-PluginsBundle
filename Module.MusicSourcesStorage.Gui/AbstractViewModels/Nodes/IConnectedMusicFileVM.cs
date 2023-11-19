using System.Windows.Input;
using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

public interface IConnectedMusicFileVM :
    IConnectedNodeVM,
    IMarkableAsListenedVM
{
    MusicFileLocation Location { get; }

    ICommand DeleteAndMarkAsListened { get; }
}