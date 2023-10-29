using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Enums;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.WizardSteps;

[AddINotifyPropertyChangedInterface]
public sealed class MusicSourceAdditionalInfoStepDTVM : IMusicSourceAdditionalInfoStepVM
{
    public string Name { get; set; } = string.Empty;
    public bool IsValidState => true;

    public StepResult Confirm()
    {
        return StepResult.Success;
    }
}