namespace Module.MusicSourcesStorage.Gui.AbstractViewModels;

public interface INodeVM
{
    string Name { get; }
    IReadOnlyList<INodeVM> ChildNodes { get; }
}