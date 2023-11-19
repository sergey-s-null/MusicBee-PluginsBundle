using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Gui.Factories;

public delegate IMusicSourceVM MusicSourceVMFactory(
    int musicSourceId,
    MusicSourceAdditionalInfo additionalInfo,
    MusicSourceType type,
    INodesHierarchyVM<IConnectedNodeVM> items
);