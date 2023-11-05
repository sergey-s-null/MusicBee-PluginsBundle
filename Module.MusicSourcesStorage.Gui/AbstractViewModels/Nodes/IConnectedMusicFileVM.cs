using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

public interface IConnectedMusicFileVM : IMusicFileVM, IDownloadableVM
{
    bool CanDelete { get; }

    MusicFileState State { get; }

    ICommand SwitchListenedState { get; }
    ICommand DeleteAndMarkAsListened { get; }
    ICommand Delete { get; }
}