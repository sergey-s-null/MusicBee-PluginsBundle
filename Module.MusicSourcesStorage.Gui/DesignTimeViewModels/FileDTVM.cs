using Module.MusicSourcesStorage.Gui.AbstractViewModels;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels;

public sealed class FileDTVM : INodeVM
{
    public string Name { get; }
    public IList<INodeVM> ChildNodes { get; } = Array.Empty<INodeVM>();

    public FileDTVM(string name)
    {
        Name = name;
    }
}