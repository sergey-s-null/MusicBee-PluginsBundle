using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.Nodes;

public sealed class ConnectedUnknownFileDTVM : UnknownFileDTVM, IConnectedUnknownFileVM
{
    public ConnectedUnknownFileDTVM()
    {
    }

    public ConnectedUnknownFileDTVM(string path) : base(path)
    {
    }
}