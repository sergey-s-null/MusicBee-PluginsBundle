using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

[AddINotifyPropertyChangedInterface]
public sealed class SuccessResultStepVM : ManualStepBaseVM, ISuccessResultStepVM
{
    public override bool CanSafelyCloseWizard { get; protected set; }

    public override bool HasNextStep { get; protected set; }
    public override bool CanGoNext { get; protected set; }
    public override string? CustomNextStepName { get; protected set; }

    public override bool HasPreviousStep { get; protected set; }
    public override bool CanGoBack { get; protected set; }

    public override string? CustomCloseWizardCommandName { get; protected set; }

    public string Text { get; }

    public SuccessResultStepVM(string text)
    {
        CanSafelyCloseWizard = true;
        HasNextStep = false;
        CanGoNext = false;
        CustomNextStepName = null;
        HasPreviousStep = false;
        CanGoBack = false;
        CustomCloseWizardCommandName = "Done";
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