using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.Factories;
using Module.MusicSourcesStorage.Gui.Services.Abstract;
using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.Services;

public sealed class ConnectedFileVMBuilder : IConnectedFileVMBuilder
{
    private readonly ConnectedMusicFileVMFactory _connectedMusicFileVMFactory;
    private readonly ConnectedImageFileVMFactory _connectedImageFileVMFactory;
    private readonly ConnectedUnknownFileVMFactory _connectedUnknownFileVMFactory;

    public ConnectedFileVMBuilder(
        ConnectedMusicFileVMFactory connectedMusicFileVMFactory,
        ConnectedImageFileVMFactory connectedImageFileVMFactory,
        ConnectedUnknownFileVMFactory connectedUnknownFileVMFactory)
    {
        _connectedMusicFileVMFactory = connectedMusicFileVMFactory;
        _connectedImageFileVMFactory = connectedImageFileVMFactory;
        _connectedUnknownFileVMFactory = connectedUnknownFileVMFactory;
    }

    public IConnectedNodeVM Build(SourceFile sourceFile)
    {
        return sourceFile switch
        {
            MusicFile musicFile => _connectedMusicFileVMFactory(musicFile),
            ImageFile imageFile => _connectedImageFileVMFactory(imageFile),
            UnknownFile unknownFile => _connectedUnknownFileVMFactory(unknownFile),
            _ => throw new ArgumentOutOfRangeException(nameof(sourceFile))
        };
    }
}