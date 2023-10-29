using Autofac;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.Services.Abstract;
using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.Services;

public sealed class ConnectedFileVMBuilder : IFileVMBuilder
{
    private readonly ILifetimeScope _lifetimeScope;

    public ConnectedFileVMBuilder(ILifetimeScope lifetimeScope)
    {
        _lifetimeScope = lifetimeScope;
    }

    public IFileVM Build(SourceFile sourceFile)
    {
        return sourceFile switch
        {
            MusicFile musicFile => Build(musicFile),
            ImageFile imageFile => Build(imageFile),
            UnknownFile unknownFile => Build(unknownFile),
            _ => throw new ArgumentOutOfRangeException(nameof(sourceFile))
        };
    }

    private IConnectedMusicFileVM Build(MusicFile musicFile)
    {
        return _lifetimeScope.Resolve<IConnectedMusicFileVM>(
            new NamedParameter("path", musicFile.Path)
        );
    }

    private IConnectedImageFileVM Build(ImageFile imageFile)
    {
        return _lifetimeScope.Resolve<IConnectedImageFileVM>(
            new NamedParameter("path", imageFile.Path)
        );
    }

    private IConnectedUnknownFileVM Build(UnknownFile unknownFile)
    {
        return _lifetimeScope.Resolve<IConnectedUnknownFileVM>(
            new NamedParameter("path", unknownFile.Path)
        );
    }
}