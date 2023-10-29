using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.Services.Abstract;

public interface IWizardService
{
    // todo dont pass wizard type - use named methods instead
    void Open(WizardType wizardType);
}