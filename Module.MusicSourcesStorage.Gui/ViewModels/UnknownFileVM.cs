using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels;

[AddINotifyPropertyChangedInterface]
public class UnknownFileVM : IUnknownFileVM
{
    public string Name { get; }
    public string Path { get; }
    public IReadOnlyList<INodeVM> ChildNodes { get; } = Array.Empty<INodeVM>();

    public UnknownFileVM(string name, string path)
    {
        Name = name;
        Path = path;
    }
}