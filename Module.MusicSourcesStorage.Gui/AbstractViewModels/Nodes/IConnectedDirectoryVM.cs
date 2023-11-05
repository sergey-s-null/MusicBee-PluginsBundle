using System.IO;
using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

public interface IConnectedDirectoryVM : IDirectoryVM, IDownloadableVM
{
    DirectoryListenedState ListenedState { get; }

    Stream? CoverStream { get; }

    ICommand MarkAllAsListened { get; }
    ICommand MarkAllAsNotListened { get; }
}