using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

public sealed class SuccessResultStepVM : ManualStepBaseVM, ISuccessResultStepVM
{
    public override bool CanSafelyCloseWizard => true;

    public override bool HasNextStep => false;
    public override bool CanGoNext => false;
    public override string? CustomNextStepName => null;

    public override bool HasPreviousStep => false;
    public override bool CanGoBack => false;

    public override string? CustomCloseWizardCommandName => "Done";

    public string Text { get; }

    public SuccessResultStepVM(string text)
    {
        Text = text;
    }

    protected override IWizardStepVM GetNextStep()
    {
        throw new InvalidOperationException();
    }

    protected override IWizardStepVM GetPreviousStep()
    {
        throw new InvalidOperationException();
    }
}