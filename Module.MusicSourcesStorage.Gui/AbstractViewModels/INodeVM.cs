namespace Module.MusicSourcesStorage.Gui.AbstractViewModels;

public interface INodeVM
{
    string Name { get; }
    IList<INodeVM> ChildNodes { get; }
}