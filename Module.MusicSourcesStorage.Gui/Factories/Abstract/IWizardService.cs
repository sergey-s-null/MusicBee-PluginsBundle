using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.Factories.Abstract;

public interface IWizardService
{
    void Open(WizardType wizardType);
}