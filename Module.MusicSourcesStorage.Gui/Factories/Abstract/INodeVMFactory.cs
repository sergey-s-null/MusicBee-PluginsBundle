using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

namespace Module.MusicSourcesStorage.Gui.Factories.Abstract;

[Obsolete("Use keyed mapper")]
public interface INodeVMFactory
{
    IDirectoryVM CreateDirectoryVM(string name, IReadOnlyList<INodeVM>? childNodes = null);

    IMusicFileVM CreateMusicFileVM(string name, string path);

    IImageFileVM CreateImageFileVM(string name, string path);

    IUnknownFileVM CreateUnknownFileVM(string name, string path);
}