using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Factories.Abstract;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

public sealed class AddMusicSourceToDatabaseStepVM : ProcessingStepBaseVM
{
    public override bool CanSafelyCloseWizard { get; protected set; }

    public override string Text { get; protected set; }

    public AddMusicSourceToDatabaseStepVM(IWizardCommonStepsFactory commonStepsFactory) : base(commonStepsFactory)
    {
        CanSafelyCloseWizard = false;
        Text = "Adding music source to database";
    }

    protected override Task<IWizardStepVM> ProcessAsync(CancellationToken token)
    {
        throw new NotImplementedException();
    }
}