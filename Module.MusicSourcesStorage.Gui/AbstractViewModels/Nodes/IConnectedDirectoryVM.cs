using System.IO;
using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

public interface IConnectedDirectoryVM : IDirectoryVM
{
    bool CanDownload { get; }
    DirectoryListenedState ListenedState { get; }

    Stream? CoverStream { get; }

    ICommand Download { get; }
    ICommand MarkAllAsListened { get; }
    ICommand MarkAllAsNotListened { get; }
}