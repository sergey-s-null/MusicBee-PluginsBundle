using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

[AddINotifyPropertyChangedInterface]
public sealed class SuccessResultStepVM : ISuccessResultStepVM
{
    public bool IsValidState => true;

    public string Text { get; }

    public SuccessResultStepVM(IWizardSuccessResultContext context)
    {
        Text = context.SuccessResultText ?? "Success";
    }

    public StepResult Confirm()
    {
        return StepResult.Success;
    }
}