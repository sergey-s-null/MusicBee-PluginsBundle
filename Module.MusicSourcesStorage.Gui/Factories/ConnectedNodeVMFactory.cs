using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.Factories.Abstract;

namespace Module.MusicSourcesStorage.Gui.Factories;

public sealed class ConnectedNodeVMFactory : INodeVMFactory
{
    private readonly ConnectedDirectoryVMFactory _connectedDirectoryVMFactory;
    private readonly ConnectedMusicFileVMFactory _connectedMusicFileVMFactory;
    private readonly ConnectedImageFileVMFactory _connectedImageFileVMFactory;
    private readonly ConnectedUnknownFileVMFactory _connectedUnknownFileVMFactory;

    public ConnectedNodeVMFactory(
        ConnectedDirectoryVMFactory connectedDirectoryVMFactory,
        ConnectedMusicFileVMFactory connectedMusicFileVMFactory,
        ConnectedImageFileVMFactory connectedImageFileVMFactory,
        ConnectedUnknownFileVMFactory connectedUnknownFileVMFactory)
    {
        _connectedDirectoryVMFactory = connectedDirectoryVMFactory;
        _connectedMusicFileVMFactory = connectedMusicFileVMFactory;
        _connectedImageFileVMFactory = connectedImageFileVMFactory;
        _connectedUnknownFileVMFactory = connectedUnknownFileVMFactory;
    }

    public IDirectoryVM CreateDirectoryVM(string name, IReadOnlyList<INodeVM>? childNodes = null)
    {
        return _connectedDirectoryVMFactory(name, childNodes ?? Array.Empty<INodeVM>());
    }

    public IMusicFileVM CreateMusicFileVM(string name, string path)
    {
        return _connectedMusicFileVMFactory(name, path);
    }

    public IImageFileVM CreateImageFileVM(string name, string path)
    {
        return _connectedImageFileVMFactory(name, path);
    }

    public IUnknownFileVM CreateUnknownFileVM(string name, string path)
    {
        return _connectedUnknownFileVMFactory(name, path);
    }
}