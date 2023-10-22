using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;

public interface IVkDocumentVM
{
    string Name { get; }

    VkDocument UnderlyingModel { get; }
}