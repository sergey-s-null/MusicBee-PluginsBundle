﻿using System.IO;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.Factories;
using Module.MusicSourcesStorage.Gui.Services.Abstract;
using Module.MusicSourcesStorage.Gui.ViewModels;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Factories.Abstract;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Gui.Services;

public sealed class NotConnectedNodesHierarchyVMBuilder : INotConnectedNodesHierarchyVMBuilder
{
    private readonly DirectoryVMFactory _directoryVMFactory;
    private readonly IHierarchyBuilder<SourceFile, string> _hierarchyBuilder;
    private readonly IFileVMBuilder _fileVMBuilder;

    public NotConnectedNodesHierarchyVMBuilder(
        DirectoryVMFactory directoryVMFactory,
        IHierarchyBuilderFactory hierarchyBuilderFactory,
        IFileVMBuilder fileVMBuilder)
    {
        _directoryVMFactory = directoryVMFactory;
        _hierarchyBuilder = hierarchyBuilderFactory.Create<SourceFile, string>(
            x => x.Path.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar),
            StringComparer.InvariantCultureIgnoreCase
        );
        _fileVMBuilder = fileVMBuilder;
    }

    public INodesHierarchyVM Build(IReadOnlyList<SourceFile> files)
    {
        _hierarchyBuilder.Build(files, out var rootNodes, out var rootLeaves);

        return new NodesHierarchyVM(CreateNodeViewModels(rootNodes, rootLeaves));
    }

    private IReadOnlyList<INodeVM> CreateNodeViewModels(
        IReadOnlyList<Node<SourceFile, string>> nodes,
        IReadOnlyList<Leaf<SourceFile, string>> leaves)
    {
        return nodes
            .Select(CreateNodeVM)
            .Concat(leaves
                .Select(x => _fileVMBuilder.Build(x.Value)))
            .ToList();
    }

    private INodeVM CreateNodeVM(Node<SourceFile, string> node)
    {
        var childNodes = CreateNodeViewModels(node.ChildNodes, node.Leaves);
        return _directoryVMFactory(node.Path[node.Path.Count - 1], childNodes);
    }
}