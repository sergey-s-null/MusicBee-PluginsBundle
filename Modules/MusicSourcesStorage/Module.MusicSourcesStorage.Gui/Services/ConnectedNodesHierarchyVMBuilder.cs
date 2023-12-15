using System.IO;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Gui.Factories;
using Module.MusicSourcesStorage.Gui.Services.Abstract;
using Module.MusicSourcesStorage.Gui.ViewModels;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Factories.Abstract;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Gui.Services;

public sealed class ConnectedNodesHierarchyVMBuilder : IConnectedNodesHierarchyVMBuilder
{
    private readonly ConnectedDirectoryVMFactory _directoryVMFactory;
    private readonly IHierarchyBuilder<SourceFile, string> _hierarchyBuilder;
    private readonly IConnectedFileVMBuilder _connectedFileVMBuilder;

    public ConnectedNodesHierarchyVMBuilder(
        ConnectedDirectoryVMFactory directoryVMFactory,
        IHierarchyBuilderFactory hierarchyBuilderFactory,
        IConnectedFileVMBuilder connectedFileVMBuilder)
    {
        _directoryVMFactory = directoryVMFactory;
        _hierarchyBuilder = hierarchyBuilderFactory.Create<SourceFile, string>(
            x => x.Path.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar),
            StringComparer.InvariantCultureIgnoreCase
        );
        _connectedFileVMBuilder = connectedFileVMBuilder;
    }

    public INodesHierarchyVM<IConnectedNodeVM> Build(int sourceId, IReadOnlyList<SourceFile> files)
    {
        _hierarchyBuilder.Build(files, out var rootNodes, out var rootLeaves);

        return new NodesHierarchyVM<IConnectedNodeVM>(CreateNodeViewModels(sourceId, rootNodes, rootLeaves));
    }

    private IReadOnlyList<IConnectedNodeVM> CreateNodeViewModels(
        int sourceId,
        IReadOnlyList<INode<SourceFile, string>> nodes,
        IReadOnlyList<ILeaf<SourceFile, string>> leaves)
    {
        return nodes
            .Select(x => CreateNodeVM(sourceId, x))
            .Concat(leaves
                .Select(x => _connectedFileVMBuilder.Build(x.Value)))
            .ToList();
    }

    private IConnectedNodeVM CreateNodeVM(int sourceId, INode<SourceFile, string> node)
    {
        var path = string.Join(Path.DirectorySeparatorChar.ToString(), node.Path);
        var childNodes = CreateNodeViewModels(sourceId, node.ChildNodes, node.Leaves);
        return _directoryVMFactory(sourceId, path, childNodes);
    }
}