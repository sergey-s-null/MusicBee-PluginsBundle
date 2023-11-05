using System.Windows.Input;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

public interface IConnectedUnknownFileVM : IUnknownFileVM, IDownloadableVM
{
    ICommand Delete { get; }
}