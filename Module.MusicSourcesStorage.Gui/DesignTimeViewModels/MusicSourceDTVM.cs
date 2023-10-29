using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels;

public sealed class MusicSourceDTVM : IMusicSourceVM
{
    public string Name { get; }
    public MusicSourceType Type { get; }
    public INodesHierarchyVM Items { get; }

    public ICommand Edit => null!;

    // ReSharper disable once UnusedMember.Global
    public MusicSourceDTVM() : this("Some Source", MusicSourceType.VkPostWithArchive)
    {
    }

    public MusicSourceDTVM(string name, MusicSourceType type, INodesHierarchyVM? items = null)
    {
        Name = name;
        Type = type;
        Items = items ?? NodesHierarchyDTVM.Empty;
    }
}