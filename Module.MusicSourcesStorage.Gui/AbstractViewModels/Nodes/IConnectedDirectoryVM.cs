using System.IO;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

public interface IConnectedDirectoryVM : IDirectoryVM, IDownloadableVM, IMarkableAsListenedVM, IDeletableVM
{
    Stream? CoverStream { get; }
}