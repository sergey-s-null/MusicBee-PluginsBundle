using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.Enums;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels;

[AddINotifyPropertyChangedInterface]
public sealed class MusicSourceVM : IMusicSourceVM
{
    public string Name { get; }
    public MusicSourceType Type { get; }
    public INodesHierarchyVM Items { get; }

    public MusicSourceVM(
        string name,
        MusicSourceType type,
        INodesHierarchyVM items)
    {
        Name = name;
        Type = type;
        Items = items;
    }
}