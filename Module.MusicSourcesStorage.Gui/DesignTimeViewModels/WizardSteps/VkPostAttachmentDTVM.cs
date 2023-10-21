using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.WizardSteps;

public sealed class VkPostAttachmentDTVM : IVkPostAttachmentVM
{
    public string Name { get; }

    public VkPostAttachmentDTVM(string name)
    {
        Name = name;
    }
}