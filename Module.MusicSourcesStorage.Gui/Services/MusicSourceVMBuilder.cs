using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.Services.Abstract;
using Module.MusicSourcesStorage.Gui.ViewModels;
using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.Services;

public sealed class MusicSourceVMBuilder : IMusicSourceVMBuilder
{
    private readonly INodesHierarchyVMBuilder _nodesHierarchyVMBuilder;

    public MusicSourceVMBuilder(INodesHierarchyVMBuilder nodesHierarchyVMBuilder)
    {
        _nodesHierarchyVMBuilder = nodesHierarchyVMBuilder;
    }

    public IMusicSourceVM Build(MusicSource musicSource)
    {
        return new MusicSourceVM(
            musicSource.Name,
            musicSource.Type,
            _nodesHierarchyVMBuilder.Build(musicSource.Files)
        );
    }
}