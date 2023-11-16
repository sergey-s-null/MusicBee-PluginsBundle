using Autofac;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.Services.Abstract;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Gui.Services;

public sealed class MusicSourceVMBuilder : IMusicSourceVMBuilder
{
    private readonly ILifetimeScope _lifetimeScope;
    private readonly IConnectedNodesHierarchyVMBuilder _nodesHierarchyVMBuilder;

    public MusicSourceVMBuilder(
        ILifetimeScope lifetimeScope,
        IConnectedNodesHierarchyVMBuilder nodesHierarchyVMBuilder)
    {
        _lifetimeScope = lifetimeScope;
        _nodesHierarchyVMBuilder = nodesHierarchyVMBuilder;
    }

    public IMusicSourceVM Build(MusicSource musicSource)
    {
        return _lifetimeScope.Resolve<IMusicSourceVM>(
            new TypedParameter(typeof(int), musicSource.Id),
            new TypedParameter(typeof(MusicSourceAdditionalInfo), musicSource.AdditionalInfo),
            new TypedParameter(typeof(MusicSourceType), musicSource.Type),
            new TypedParameter(
                typeof(INodesHierarchyVM),
                _nodesHierarchyVMBuilder.Build(musicSource.Id, musicSource.Files)
            )
        );
    }
}