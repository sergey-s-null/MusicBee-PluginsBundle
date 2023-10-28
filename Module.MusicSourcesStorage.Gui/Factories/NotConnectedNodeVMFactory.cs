using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.Factories.Abstract;
using Module.MusicSourcesStorage.Gui.ViewModels.Nodes;

namespace Module.MusicSourcesStorage.Gui.Factories;

public sealed class NotConnectedNodeVMFactory : INodeVMFactory
{
    public IDirectoryVM CreateDirectoryVM(string name, IReadOnlyList<INodeVM>? childNodes)
    {
        return new DirectoryVM(name, childNodes ?? Array.Empty<INodeVM>());
    }

    public IMusicFileVM CreateMusicFileVM(string name, string path)
    {
        return new MusicFileVM(name, path);
    }

    public IImageFileVM CreateImageFileVM(string name, string path)
    {
        return new ImageFileVM(name, path);
    }

    public IUnknownFileVM CreateUnknownFileVM(string name, string path)
    {
        return new UnknownFileVM(name, path);
    }
}