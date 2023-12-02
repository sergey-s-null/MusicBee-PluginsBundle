using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Enums;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

[AddINotifyPropertyChangedInterface]
public sealed class SuccessResultStepVM : ISuccessResultStepVM
{
    public bool IsValidState => true;

    public string Text { get; }

    public SuccessResultStepVM() : this("Success")
    {
    }

    public SuccessResultStepVM(string text)
    {
        Text = text;
    }

    public StepResult Confirm()
    {
        return StepResult.Success;
    }
}