using System.IO;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

public interface IConnectedDirectoryVM :
    IDirectoryVM,
    IProcessableVM,
    IDownloadableVM,
    IMarkableAsListenedVM,
    IDeletableVM
{
    bool HasDownloadedAndNotAttachedToLibraryFiles { get; }

    Stream? CoverStream { get; }
}