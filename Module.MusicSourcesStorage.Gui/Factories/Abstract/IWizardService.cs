using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Gui.Views;

namespace Module.MusicSourcesStorage.Gui.Factories.Abstract;

public interface IWizardService
{
    void Open(WizardType wizardType);
}