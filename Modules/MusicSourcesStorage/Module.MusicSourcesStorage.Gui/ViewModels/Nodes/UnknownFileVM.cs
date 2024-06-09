using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.Nodes;

[AddINotifyPropertyChangedInterface]
public sealed class UnknownFileVM : FileBaseVM, IUnknownFileVM
{
    public override string Name { get; }
    public override string Path { get; }

    public UnknownFileVM(string path)
    {
        Name = System.IO.Path.GetFileName(path);
        Path = path;
    }
}