using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels;

public sealed class MusicSourceDTVM : IMusicSourceVM
{
#pragma warning disable CS0067
    public event EventHandler? Deleted;
#pragma warning restore

    public string Name { get; }
    public MusicSourceType Type { get; }
    public INodesHierarchyVM<IConnectedNodeVM> Items { get; }

    public ICommand Edit => null!;
    public ICommand Delete => null!;

    public MusicSourceDTVM() : this("Some Source", MusicSourceType.VkPostWithArchive)
    {
    }

    public MusicSourceDTVM(string name, MusicSourceType type, INodesHierarchyVM<IConnectedNodeVM>? items = null)
    {
        Name = name;
        Type = type;
        Items = items ?? NodesHierarchyDTVM.EmptyConnected;
    }
}