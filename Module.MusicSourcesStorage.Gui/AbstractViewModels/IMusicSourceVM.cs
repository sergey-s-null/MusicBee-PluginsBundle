using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels;

public interface IMusicSourceVM
{
    event EventHandler Deleted;

    string Name { get; }
    MusicSourceType Type { get; }
    INodesHierarchyVM<IConnectedNodeVM> Items { get; }

    ICommand Edit { get; }
    ICommand Delete { get; }
}