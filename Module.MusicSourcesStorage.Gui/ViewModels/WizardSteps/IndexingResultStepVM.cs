using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

public sealed class IndexingResultStepVM : IIndexingResultStepVM
{
    public bool IsValidState => true;

    public INodesHierarchyVM Items { get; }

    public IndexingResultStepVM(INodesHierarchyVM items)
    {
        Items = items;
    }

    public StepResult Confirm()
    {
        return StepResult.Success;
    }
}