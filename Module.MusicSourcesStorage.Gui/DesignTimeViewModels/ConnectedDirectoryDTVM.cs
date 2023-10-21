using Module.MusicSourcesStorage.Gui.AbstractViewModels;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels;

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