using System.Windows.Input;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

public interface IConnectedImageFileVM :
    IImageFileVM,
    IProcessableVM,
    IDownloadableVM,
    IDeletableVM
{
    bool IsCover { get; }

    ICommand SelectAsCover { get; }
}