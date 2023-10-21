using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.WizardSteps;

public sealed class SelectVkPostAttachmentStepDTVM : ISelectVkPostAttachmentStepVM
{
    public event EventHandler<StepTransitionEventArgs>? StepTransitionRequested;
    public event EventHandler? CloseWizardRequested;

    public bool HasNextStep => true;
    public bool CanGoNext => false;
    public string? CustomNextStepName => null;

    public bool HasPreviousStep => true;
    public bool CanGoBack => true;

    public ICommand Back => null!;
    public ICommand Next => null!;
    public ICommand CloseWizard => null!;

    public ulong PostOwnerId => 2323;
    public ulong PostId => 600909;

    public IReadOnlyList<IVkPostAttachmentVM> Attachments { get; }

    public SelectVkPostAttachmentStepDTVM()
    {
        var attachments = new List<IVkPostAttachmentVM>
        {
            new VkPostAttachmentDTVM("Attachment1"),
            new VkPostAttachmentDTVM("Music_flac.zip"),
            new VkPostAttachmentDTVM("Music_mp3.rar"),
        };

        for (var i = 0; i < 100; i++)
        {
            attachments.Add(new VkPostAttachmentDTVM("empty.rar"));
        }

        Attachments = attachments;
    }
}