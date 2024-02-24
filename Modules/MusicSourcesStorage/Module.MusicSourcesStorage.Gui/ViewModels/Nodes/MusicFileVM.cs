using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.Nodes;

[AddINotifyPropertyChangedInterface]
public sealed class MusicFileVM : FileBaseVM, IMusicFileVM
{
    public override string Name { get; }
    public override string Path { get; }

    public MusicFileVM(string path)
    {
        Name = System.IO.Path.GetFileName(path);
        Path = path;
    }
}