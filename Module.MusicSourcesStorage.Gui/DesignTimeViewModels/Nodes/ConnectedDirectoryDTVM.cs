using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.Nodes;

public sealed class ConnectedDirectoryDTVM : DirectoryDTVM, IConnectedDirectoryVM
{
    public ConnectedDirectoryDTVM()
    {
    }

    public ConnectedDirectoryDTVM(string name) : base(name)
    {
    }

    public ConnectedDirectoryDTVM(string name, IReadOnlyList<INodeVM> childNodes, string? coverFileName = null)
        : base(name, childNodes, coverFileName)
    {
    }
}