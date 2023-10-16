using Module.MusicSourcesStorage.Gui.AbstractViewModels;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels;

public sealed record DirectoryDTVM(string Name, IList<INodeVM> ChildNodes) : INodeVM
{
    public DirectoryDTVM(string name) : this(name, Array.Empty<INodeVM>())
    {
    }
}