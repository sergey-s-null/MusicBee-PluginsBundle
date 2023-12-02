using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.Entities.Abstract;

public interface IWizardPipelines
{
    IWizardStepDescriptor GetRootDescriptor(WizardType wizardType);
}