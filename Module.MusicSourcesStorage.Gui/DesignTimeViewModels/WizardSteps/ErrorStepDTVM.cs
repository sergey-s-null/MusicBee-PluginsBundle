using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.WizardSteps;

public sealed class ErrorStepDTVM : IErrorStepVM
{
    public bool IsValidState => true;

    public string Error => DesignTimeData.BigText;

    public StepResult Confirm()
    {
        throw new NotImplementedException();
    }
}