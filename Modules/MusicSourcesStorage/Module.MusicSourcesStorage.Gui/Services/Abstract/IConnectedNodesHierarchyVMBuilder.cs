using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.Services.Abstract;

public interface IConnectedNodesHierarchyVMBuilder
{
    INodesHierarchyVM<IConnectedNodeVM> Build(int sourceId, IReadOnlyList<SourceFile> files);
}