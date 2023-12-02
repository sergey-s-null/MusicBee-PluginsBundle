using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels;

public static class NodesHierarchyDTVM
{
    public static readonly INodesHierarchyVM<INodeVM> Empty =
        new NodesHierarchyDTVM<INodeVM>(Array.Empty<INodeVM>());

    public static readonly INodesHierarchyVM<IConnectedNodeVM> EmptyConnected =
        new NodesHierarchyDTVM<IConnectedNodeVM>(Array.Empty<IConnectedNodeVM>());

    public static readonly INodesHierarchyVM<INodeVM> NotConnectedAllTypes =
        new NodesHierarchyDTVM<INodeVM>(DesignTimeData.NotConnectedAllTypesNodes);

    public static readonly INodesHierarchyVM<IConnectedNodeVM> ConnectedAllTypes =
        new NodesHierarchyDTVM<IConnectedNodeVM>(DesignTimeData.ConnectedAllTypesNodes);
}

public sealed class NodesHierarchyDTVM<TNode> : INodesHierarchyVM<TNode>
{
    public IReadOnlyList<TNode> RootNodes { get; }

    public NodesHierarchyDTVM(IReadOnlyList<TNode> rootNodes)
    {
        RootNodes = rootNodes;
    }
}