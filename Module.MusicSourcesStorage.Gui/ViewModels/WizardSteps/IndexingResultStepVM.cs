using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

public sealed class IndexingResultStepVM : ManualStepBaseVM, IIndexingResultStepVM
{
    public override bool CanSafelyCloseWizard { get; protected set; }

    public override bool HasNextStep { get; protected set; }
    public override bool CanGoNext { get; protected set; }
    public override string? CustomNextStepName { get; protected set; }

    public override bool HasPreviousStep { get; protected set; }
    public override bool CanGoBack { get; protected set; }

    public override string? CustomCloseWizardCommandName { get; protected set; }

    public INodesHierarchyVM Items { get; }

    public IndexingResultStepVM(INodesHierarchyVM items)
    {
        CanSafelyCloseWizard = false;
        HasNextStep = true;
        CanGoNext = true;
        CustomNextStepName = "Add";
        HasPreviousStep = true;
        CanGoBack = true;
        CustomCloseWizardCommandName = null;

        Items = items;
    }

    protected override IWizardStepVM GetNextStep()
    {
        throw new NotImplementedException();
    }

    protected override IWizardStepVM GetPreviousStep()
    {
        throw new NotImplementedException();
    }
}