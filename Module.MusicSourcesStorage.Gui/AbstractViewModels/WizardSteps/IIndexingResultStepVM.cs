using Module.MusicSourcesStorage.Gui.AbstractViewModels.Nodes;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;

public interface IIndexingResultStepVM : IManualWizardStepVM
{
    INodesHierarchyVM<INodeVM> Items { get; }
}