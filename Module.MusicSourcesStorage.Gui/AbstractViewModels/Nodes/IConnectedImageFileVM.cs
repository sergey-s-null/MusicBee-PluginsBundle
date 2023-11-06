using System.Windows.Input;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

public interface IConnectedImageFileVM : IImageFileVM, IDownloadableVM, IDeletableVM
{
    bool IsCover { get; }

    ICommand SelectAsCover { get; }
}