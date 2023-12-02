using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;
using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.Extensions;

public static class LogicToVmMappingExtensions
{
    public static IVkDocumentVM ToViewModel(this VkDocument document)
    {
        return new VkDocumentVM(document.Name);
    }
}