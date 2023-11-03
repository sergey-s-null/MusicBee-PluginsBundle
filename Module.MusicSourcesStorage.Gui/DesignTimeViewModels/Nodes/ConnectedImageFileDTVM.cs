using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.Nodes;

public sealed class ConnectedImageFileDTVM : ImageFileDTVM, IConnectedImageFileVM
{
    public ConnectedImageFileDTVM()
    {
    }

    public ConnectedImageFileDTVM(string path, bool isCover) : base(path)
    {
    }
}