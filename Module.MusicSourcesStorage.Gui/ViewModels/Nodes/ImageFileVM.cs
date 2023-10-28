using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.Nodes;

[AddINotifyPropertyChangedInterface]
public class ImageFileVM : IImageFileVM
{
    public string Name { get; }

    public string Path { get; }

    // todo move to connected?
    public bool IsCover { get; }

    public IReadOnlyList<INodeVM> ChildNodes { get; } = Array.Empty<INodeVM>();

    public ImageFileVM(string name, string path)
    {
        Name = name;
        Path = path;
        IsCover = false;
    }
}