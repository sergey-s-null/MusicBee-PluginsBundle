using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.Factories;
using Module.MusicSourcesStorage.Gui.Services.Abstract;
using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.Services;

public sealed class MusicSourceVMBuilder : IMusicSourceVMBuilder
{
    private readonly MusicSourceVMFactory _musicSourceVMFactory;
    private readonly IConnectedNodesHierarchyVMBuilder _nodesHierarchyVMBuilder;

    public MusicSourceVMBuilder(
        MusicSourceVMFactory musicSourceVMFactory,
        IConnectedNodesHierarchyVMBuilder nodesHierarchyVMBuilder)
    {
        _musicSourceVMFactory = musicSourceVMFactory;
        _nodesHierarchyVMBuilder = nodesHierarchyVMBuilder;
    }

    public IMusicSourceVM Build(MusicSource musicSource)
    {
        return _musicSourceVMFactory(
            musicSource.Id,
            musicSource.AdditionalInfo,
            musicSource.Type,
            _nodesHierarchyVMBuilder.Build(musicSource.Id, musicSource.Files)
        );
    }
}