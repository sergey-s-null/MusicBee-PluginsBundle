using System.IO;
using Autofac.Features.AttributeFilters;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.Factories;
using Module.MusicSourcesStorage.Gui.Services.Abstract;
using Module.MusicSourcesStorage.Gui.ViewModels;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Enums;
using Module.MusicSourcesStorage.Logic.Factories.Abstract;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Gui.Services;

public sealed class NodesHierarchyVMBuilder : INodesHierarchyVMBuilder
{
    private readonly DirectoryVMFactory _directoryVMFactory;
    private readonly IHierarchyBuilder<SourceFile, string> _hierarchyBuilder;
    private readonly IFileVMBuilder _fileVMBuilder;

    public NodesHierarchyVMBuilder(
        DirectoryVMFactory directoryVMFactory,
        [KeyFilter(HierarchyMode.Lazy)]
        IHierarchyBuilderFactory hierarchyBuilderFactory,
        IFileVMBuilder fileVMBuilder)
    {
        _directoryVMFactory = directoryVMFactory;
        _hierarchyBuilder = hierarchyBuilderFactory.Create<SourceFile, string>(
            x => x.Path.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar),
            StringComparer.InvariantCultureIgnoreCase,
            LeavesGroupingConfiguration.Default
        );
        _fileVMBuilder = fileVMBuilder;
    }

    public INodesHierarchyVM<INodeVM> Build(IReadOnlyList<SourceFile> files)
    {
        _hierarchyBuilder.Build(files, out var rootNodes, out var rootLeaves);

        return new NodesHierarchyVM<INodeVM>(CreateNodeViewModels(rootNodes, rootLeaves));
    }

    private IReadOnlyList<INodeVM> CreateNodeViewModels(
        IReadOnlyList<INode<SourceFile, string>> nodes,
        IReadOnlyList<ILeaf<SourceFile, string>> leaves)
    {
        return nodes
            .Select(CreateNodeVM)
            .Concat(leaves
                .Select(x => _fileVMBuilder.Build(x.Value)))
            .ToList();
    }

    private INodeVM CreateNodeVM(INode<SourceFile, string> node)
    {
        var childNodes = CreateNodeViewModels(node.ChildNodes, node.Leaves);
        var path = string.Join(Path.DirectorySeparatorChar.ToString(), node.Path);
        return _directoryVMFactory(path, childNodes);
    }
}