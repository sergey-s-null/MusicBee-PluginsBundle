using System.Windows.Input;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

public interface IConnectedUnknownFileVM : IUnknownFileVM
{
    bool IsDownloaded { get; }

    ICommand Download { get; }
    ICommand Delete { get; }
}