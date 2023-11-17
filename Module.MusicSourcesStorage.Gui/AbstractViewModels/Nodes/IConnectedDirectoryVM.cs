using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

public interface IConnectedDirectoryVM :
    IDirectoryVM,
    IProcessableVM,
    IDownloadableVM,
    IMarkableAsListenedVM,
    IDeletableVM
{
    bool IsAllListened { get; }
    bool IsAllNotListened { get; }

    bool CanRemoveCover { get; }

    bool HasDownloadedAndNotAttachedToLibraryFiles { get; }

    BitmapSource? Cover { get; }

    ICommand RemoveCover { get; }
}