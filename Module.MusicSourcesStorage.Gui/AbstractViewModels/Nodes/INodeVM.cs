namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

public interface INodeVM
{
    string Name { get; }
    IReadOnlyList<INodeVM> ChildNodes { get; }
}