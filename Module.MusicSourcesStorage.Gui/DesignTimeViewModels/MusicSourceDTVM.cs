using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels;

public sealed class MusicSourceDTVM : IMusicSourceVM
{
    public string Name { get; }
    public MusicSourceType Type { get; }
    public INodesHierarchyVM Items { get; }

    // ReSharper disable once UnusedMember.Global
    public MusicSourceDTVM() : this("Some Source", MusicSourceType.VkPost)
    {
    }

    public MusicSourceDTVM(string name, MusicSourceType type, INodesHierarchyVM? items = null)
    {
        Name = name;
        Type = type;
        Items = items ?? NodesHierarchyDTVM.Empty;
    }
}