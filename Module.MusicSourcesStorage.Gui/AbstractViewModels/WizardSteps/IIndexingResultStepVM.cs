namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;

public interface IIndexingResultStepVM : IManualWizardStepVM
{
    INodesHierarchyVM Items { get; }
}