using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.WizardSteps;

public class IndexingResultStepDTVM : IIndexingResultStepVM
{
    public bool IsValidState => true;

    public INodesHierarchyVM Items { get; }


    public IndexingResultStepDTVM() : this(NodesHierarchyDTVM.NotConnectedAllTypes)
    {
    }

    public IndexingResultStepDTVM(INodesHierarchyVM items)
    {
        Items = items;
    }

    public StepResult Confirm()
    {
        throw new NotSupportedException();
    }
}