namespace Module.MusicSourcesStorage.Gui.AbstractViewModels;

public interface IMusicSourceVM
{
    string Name { get; }
    IList<INodeVM> RootElements { get; }
}