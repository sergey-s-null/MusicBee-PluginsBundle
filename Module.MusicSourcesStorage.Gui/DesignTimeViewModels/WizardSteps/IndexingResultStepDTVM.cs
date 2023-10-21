using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.WizardSteps;

public class IndexingResultStepDTVM : IIndexingResultStepVM
{
    public event EventHandler<StepTransitionEventArgs>? StepTransitionRequested;
    public event EventHandler? CloseWizardRequested;

    public bool HasNextStep => true;
    public bool CanGoNext => true;
    public string? CustomNextStepName => "Add";

    public bool HasPreviousStep => true;
    public bool CanGoBack => true;

    public ICommand Back => null!;
    public ICommand Next => null!;
    public ICommand CloseWizard => null!;

    public INodesHierarchyVM Items { get; }

    public IndexingResultStepDTVM() : this(NodesHierarchyDTVM.NotConnectedAllTypes)
    {
    }

    public IndexingResultStepDTVM(INodesHierarchyVM items)
    {
        Items = items;
    }
}