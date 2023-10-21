using Module.MusicSourcesStorage.Gui.AbstractViewModels;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels;

public sealed class NodesHierarchyDTVM : INodesHierarchyVM
{
    public static readonly INodesHierarchyVM Empty =
        new NodesHierarchyDTVM(Array.Empty<INodeVM>());

    public static readonly INodesHierarchyVM NotConnectedAllTypes =
        new NodesHierarchyDTVM(DesignTimeData.NotConnectedAllTypesNodes);

    public static readonly INodesHierarchyVM ConnectedAllTypes =
        new NodesHierarchyDTVM(DesignTimeData.ConnectedAllTypesNodes);

    public IReadOnlyList<INodeVM> RootNodes { get; }

    public NodesHierarchyDTVM() : this(DesignTimeData.NotConnectedAllTypesNodes)
    {
    }

    public NodesHierarchyDTVM(IReadOnlyList<INodeVM> rootNodes)
    {
        RootNodes = rootNodes;
    }
}