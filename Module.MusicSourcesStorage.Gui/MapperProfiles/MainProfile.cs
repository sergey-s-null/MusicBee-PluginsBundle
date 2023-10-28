using AutoMapper;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;
using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.MapperProfiles;

public sealed class MainProfile : Profile
{
    public MainProfile()
    {
        CreateMap<VkDocument, IVkDocumentVM>()
            .ConstructUsing(x => new VkDocumentVM(x.Name));
    }
}