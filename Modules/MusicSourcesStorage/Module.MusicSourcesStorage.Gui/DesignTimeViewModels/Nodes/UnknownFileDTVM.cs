using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.Nodes;

public class UnknownFileDTVM : IUnknownFileVM
{
    public string Name { get; }
    public string Path { get; }
    public IReadOnlyList<INodeVM> ChildNodes { get; } = Array.Empty<INodeVM>();

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