using Module.MusicSourcesStorage.Gui.AbstractViewModels;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels;

public sealed record DirectoryDTVM(string Name, IList<INodeVM> ChildNodes) : IDirectoryVM
{
    // ReSharper disable once UnusedMember.Global
    public DirectoryDTVM() : this("SomeDirectory")
    {
    }

    public DirectoryDTVM(string Name) : this(Name, Array.Empty<INodeVM>())
    {
    }
}