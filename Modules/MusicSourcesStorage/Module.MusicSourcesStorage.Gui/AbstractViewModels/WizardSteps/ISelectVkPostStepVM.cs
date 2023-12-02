namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;

public interface ISelectVkPostStepVM : IManualWizardStepVM
{
    string PostGlobalId { get; set; }
    bool IsValidPostGlobalId { get; }

    long? OwnerId { get; }
    long? PostId { get; }
}