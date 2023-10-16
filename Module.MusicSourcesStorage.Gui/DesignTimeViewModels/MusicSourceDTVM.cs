using Module.MusicSourcesStorage.Gui.AbstractViewModels;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels;

public sealed record MusicSourceDTVM(string Name, IList<INodeVM> RootElements) : IMusicSourceVM
{
    public MusicSourceDTVM(string name) : this(name, Array.Empty<INodeVM>())
    {
    }
}