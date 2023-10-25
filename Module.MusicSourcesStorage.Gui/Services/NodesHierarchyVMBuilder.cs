﻿using System.IO;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.Factories.Abstract;
using Module.MusicSourcesStorage.Gui.Services.Abstract;
using Module.MusicSourcesStorage.Gui.ViewModels;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Factories;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Gui.Services;

public sealed class NodesHierarchyVMBuilder : INodesHierarchyVMBuilder
{
    private readonly INodeVMFactory _nodeVMFactory;
    private readonly IHierarchyBuilder<MusicSourceFile, string> _hierarchyBuilder;
    private readonly IFileClassifier _fileClassifier;
    private readonly INodeVMBuilder _nodeVMBuilder;

    public NodesHierarchyVMBuilder(
        INodeVMFactory nodeVMFactory,
        HierarchyBuilderFactory<MusicSourceFile, string> hierarchyBuilderFactory,
        IFileClassifier fileClassifier,
        INodeVMBuilder nodeVMBuilder)
    {
        _nodeVMFactory = nodeVMFactory;
        _hierarchyBuilder = hierarchyBuilderFactory(
            x => x.Path.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar),
            StringComparer.InvariantCultureIgnoreCase
        );
        _fileClassifier = fileClassifier;
        _nodeVMBuilder = nodeVMBuilder;
    }

    public INodesHierarchyVM Build(IReadOnlyList<MusicSourceFile> files)
    {
        _hierarchyBuilder.Build(files, out var rootNodes, out var rootLeaves);

        return new NodesHierarchyVM(CreateNodeViewModels(rootNodes, rootLeaves));
    }

    private IReadOnlyList<INodeVM> CreateNodeViewModels(
        IReadOnlyList<Node<MusicSourceFile, string>> nodes,
        IReadOnlyList<Leaf<MusicSourceFile, string>> leaves)
    {
        return nodes
            .Select(CreateNodeVM)
            .Concat(leaves
                .Select(x => CreateNodeVM(x.Value))
            )
            .ToList();
    }

    private INodeVM CreateNodeVM(Node<MusicSourceFile, string> node)
    {
        var childNodes = CreateNodeViewModels(node.ChildNodes, node.Leaves);

        return _nodeVMFactory.CreateDirectoryVM(node.PathElement, childNodes);
    }

    private INodeVM CreateNodeVM(MusicSourceFile file)
    {
        var fileType = _fileClassifier.Classify(file);
        return _nodeVMBuilder.Build(file, fileType);
    }
}