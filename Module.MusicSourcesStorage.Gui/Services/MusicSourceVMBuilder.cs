﻿using Module.MusicSourcesStorage.Database.Models;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.Extensions;
using Module.MusicSourcesStorage.Gui.Services.Abstract;
using Module.MusicSourcesStorage.Gui.ViewModels;

namespace Module.MusicSourcesStorage.Gui.Services;

// todo register and think about keyed and scope
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
            musicSource.GetSourceType(),
            _nodesHierarchyVMBuilder.Build(musicSource.Files)
        );
    }
}