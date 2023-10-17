using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels;

public sealed record MusicSourceDTVM(string Name, MusicSourceType Type, IList<INodeVM> RootElements) : IMusicSourceVM
{
    // ReSharper disable once UnusedMember.Global
    public MusicSourceDTVM() : this("Some Source", MusicSourceType.VkPost)
    {
    }

    public MusicSourceDTVM(string name, MusicSourceType type) : this(name, type, Array.Empty<INodeVM>())
    {
    }
}