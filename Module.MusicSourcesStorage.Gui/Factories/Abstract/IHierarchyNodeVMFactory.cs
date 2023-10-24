using Module.MusicSourcesStorage.Gui.AbstractViewModels;

namespace Module.MusicSourcesStorage.Gui.Factories.Abstract;

public interface IHierarchyNodeVMFactory
{
    IDirectoryVM CreateDirectoryVM(string name, IReadOnlyList<INodeVM>? childNodes = null);

    IMusicFileVM CreateMusicFileVM(string name, string path);

    IImageFileVM CreateImageFileVM(string name, string path);

    IUnknownFileVM CreateUnknownFileVM(string name, string path);
}