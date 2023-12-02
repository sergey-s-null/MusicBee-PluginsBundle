using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

public sealed class VkDocumentVM : IVkDocumentVM
{
    public string Name { get; }

    public VkDocumentVM(string name)
    {
        Name = name;
    }
}