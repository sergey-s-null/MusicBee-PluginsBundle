using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

public interface IConnectedMusicFileVM : IMusicFileVM, IDownloadableVM, IMarkableAsListenedVM
{
    bool CanDelete { get; }

    MusicFileLocation Location { get; }

    ICommand DeleteAndMarkAsListened { get; }
    ICommand Delete { get; }
}