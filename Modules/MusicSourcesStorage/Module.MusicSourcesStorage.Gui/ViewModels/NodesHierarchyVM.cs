using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels;

[AddINotifyPropertyChangedInterface]
public sealed class NodesHierarchyVM<TNode> : INodesHierarchyVM<TNode>
{
    public IReadOnlyList<TNode> RootNodes { get; }

    public NodesHierarchyVM(IReadOnlyList<TNode> rootNodes)
    {
        RootNodes = rootNodes;
    }
}