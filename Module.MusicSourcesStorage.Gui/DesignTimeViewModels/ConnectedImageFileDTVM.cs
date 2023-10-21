using Module.MusicSourcesStorage.Gui.AbstractViewModels;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels;

public sealed class ConnectedImageFileDTVM : ImageFileDTVM, IConnectedImageFileVM
{
    public ConnectedImageFileDTVM()
    {
    }

    public ConnectedImageFileDTVM(string path, bool isCover) : base(path, isCover)
    {
    }
}