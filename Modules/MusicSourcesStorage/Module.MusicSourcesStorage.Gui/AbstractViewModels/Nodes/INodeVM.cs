namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

public interface INodeVM
{
    string Name { get; }
    string Path { get; }

    IReadOnlyList<INodeVM> ChildNodes { get; }
}