using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Enums;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.WizardSteps;

[AddINotifyPropertyChangedInterface]
public sealed class EditMusicSourceAdditionalInfoStepDTVM : IEditMusicSourceAdditionalInfoStepVM
{
    public string Name { get; set; } = "Some name";
    public string? NameError => null;

    public string TargetFilesDirectory { get; set; } = "C:/some/path";
    public string? TargetFilesDirectoryError => "Path is rooted";
    public bool IsValidState => true;

    public StepResult Confirm()
    {
        return StepResult.Success;
    }
}