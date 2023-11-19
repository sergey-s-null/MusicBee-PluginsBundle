using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.Nodes;

[AddINotifyPropertyChangedInterface]
public abstract class FileBaseVM : INodeVM
{
    public string Name { get; }
    public string Path { get; }

    public IReadOnlyList<INodeVM> ChildNodes { get; } = Array.Empty<INodeVM>();

    protected FileBaseVM(string path)
    {
        Name = System.IO.Path.GetFileName(path);
        Path = path;
    }
}