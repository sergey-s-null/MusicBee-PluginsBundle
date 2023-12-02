namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;

public interface IEditMusicSourceAdditionalInfoStepVM : IManualWizardStepVM
{
    string Name { get; set; }
    string? NameError { get; }

    string TargetFilesDirectory { get; set; }
    string? TargetFilesDirectoryError { get; }
}