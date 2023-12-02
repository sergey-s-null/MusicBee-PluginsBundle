using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.Services.Abstract;

public interface INodesHierarchyVMBuilder
{
    INodesHierarchyVM<INodeVM> Build(IReadOnlyList<SourceFile> files);
}