using System.Windows.Input;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

public interface IConnectedImageFileVM : IImageFileVM
{
    bool IsDownloaded { get; }
    bool IsCover { get; }

    ICommand Download { get; }
    ICommand SelectAsCover { get; }
    ICommand Delete { get; }
}