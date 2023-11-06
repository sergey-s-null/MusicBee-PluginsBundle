using Autofac;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.Factories;
using Module.MusicSourcesStorage.Gui.Services.Abstract;
using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.Services;

public sealed class ConnectedFileVMBuilder : IFileVMBuilder
{
    private readonly ILifetimeScope _lifetimeScope;
    private readonly ConnectedUnknownFileVMFactory _connectedUnknownFileVMFactory;

    public ConnectedFileVMBuilder(
        ILifetimeScope lifetimeScope,
        ConnectedUnknownFileVMFactory connectedUnknownFileVMFactory)
    {
        _lifetimeScope = lifetimeScope;
        _connectedUnknownFileVMFactory = connectedUnknownFileVMFactory;
    }

    public IFileVM Build(SourceFile sourceFile)
    {
        return sourceFile switch
        {
            MusicFile musicFile => Build(musicFile),
            ImageFile imageFile => Build(imageFile),
            UnknownFile unknownFile => _connectedUnknownFileVMFactory(unknownFile),
            _ => throw new ArgumentOutOfRangeException(nameof(sourceFile))
        };
    }

    private IConnectedMusicFileVM Build(MusicFile musicFile)
    {
        // todo use delegate factory
        return _lifetimeScope.Resolve<IConnectedMusicFileVM>(
            new NamedParameter("path", musicFile.Path)
        );
    }

    private IConnectedImageFileVM Build(ImageFile imageFile)
    {
        // todo use delegate factory
        return _lifetimeScope.Resolve<IConnectedImageFileVM>(
            new NamedParameter("path", imageFile.Path)
        );
    }
}