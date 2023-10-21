using Module.MusicSourcesStorage.Gui.AbstractViewModels;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels;

public sealed class ConnectedUnknownFileDTVM : UnknownFileDTVM, IConnectedUnknownFileVM
{
    public ConnectedUnknownFileDTVM()
    {
    }

    public ConnectedUnknownFileDTVM(string path) : base(path)
    {
    }
}