using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.WizardSteps;

public sealed class VkDocumentDTVM : IVkDocumentVM
{
    public string Name { get; }

    public VkDocumentDTVM(string name)
    {
        Name = name;
    }
}