using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.WizardSteps;

public sealed class SuccessResultStepDTVM : ISuccessResultStepVM
{
    public bool IsValidState => true;

    public string Text { get; }

    public SuccessResultStepDTVM() : this("Very Big step for Humanity")
    {
    }

    public SuccessResultStepDTVM(string text)
    {
        Text = text;
    }

    public StepResult Confirm()
    {
        throw new NotSupportedException();
    }
}