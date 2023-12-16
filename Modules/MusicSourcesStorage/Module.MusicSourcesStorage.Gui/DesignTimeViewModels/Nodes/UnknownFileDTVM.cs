using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.Nodes;

public class UnknownFileDTVM : FileBaseDTVM, IUnknownFileVM
{
    public override string Name { get; }
    public override string Path { get; }

    // ReSharper disable once UnusedMember.Global
    public UnknownFileDTVM() : this("some/path/to/unknown_file")
    {
    }

    public UnknownFileDTVM(string path)
    {
        Name = System.IO.Path.GetFileName(path);
        Path = path;
    }
}