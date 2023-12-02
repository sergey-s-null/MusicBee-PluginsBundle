namespace Module.MusicSourcesStorage.Gui.AbstractViewModels;

public interface INodesHierarchyVM<out TNode>
{
    IReadOnlyList<TNode> RootNodes { get; }
}