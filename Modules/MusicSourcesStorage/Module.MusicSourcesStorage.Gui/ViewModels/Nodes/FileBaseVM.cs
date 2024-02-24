using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.Nodes;

[AddINotifyPropertyChangedInterface]
public abstract class FileBaseVM : INodeVM
{
    public abstract string Name { get; }
    public abstract string Path { get; }

    public bool IsExpanded
    {
        get => false;
        set { }
    }

    public IReadOnlyList<INodeVM> ChildNodes { get; } = Array.Empty<INodeVM>();
}