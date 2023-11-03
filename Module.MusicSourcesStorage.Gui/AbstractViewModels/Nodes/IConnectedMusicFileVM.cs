using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

public interface IConnectedMusicFileVM : IMusicFileVM
{
    bool CanDownload { get; }
    bool CanDelete { get; }

    MusicFileState State { get; }

    ICommand Download { get; }
    ICommand SwitchListenedState { get; }
    ICommand DeleteAndMarkAsListened { get; }
    ICommand Delete { get; }
}