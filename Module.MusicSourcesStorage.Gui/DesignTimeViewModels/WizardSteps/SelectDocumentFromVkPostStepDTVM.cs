using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.WizardSteps;

public sealed class SelectDocumentFromVkPostStepDTVM : ISelectDocumentFromVkPostStepVM
{
    public bool IsValidState => true;

    public ulong PostOwnerId => 2323;
    public ulong PostId => 600909;

    public IReadOnlyList<IVkDocumentVM> Documents { get; }
    public IVkDocumentVM? SelectedDocument { get; set; }

    public SelectDocumentFromVkPostStepDTVM()
    {
        var documents = new List<IVkDocumentVM>
        {
            new VkDocumentDTVM("Document1"),
            new VkDocumentDTVM("Music_flac.zip"),
            new VkDocumentDTVM("Music_mp3.rar"),
        };

        for (var i = 0; i < 100; i++)
        {
            documents.Add(new VkDocumentDTVM("empty.rar"));
        }

        Documents = documents;
    }

    public StepResult Confirm()
    {
        throw new NotSupportedException();
    }
}