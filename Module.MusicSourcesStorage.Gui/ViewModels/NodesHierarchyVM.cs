using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels;

[AddINotifyPropertyChangedInterface]
public sealed class NodesHierarchyVM : INodesHierarchyVM
{
    public IReadOnlyList<INodeVM> RootNodes { get; }

    public NodesHierarchyVM(IReadOnlyList<INodeVM> rootNodes)
    {
        RootNodes = rootNodes;
    }
}