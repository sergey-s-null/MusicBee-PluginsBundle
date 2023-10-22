using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

[AddINotifyPropertyChangedInterface]
public sealed class SelectVkPostAttachmentStepVM : ManualStepBaseVM, ISelectVkPostAttachmentStepVM
{
    public override bool CanSafelyCloseWizard { get; protected set; }

    public override bool HasNextStep { get; protected set; }
    public override bool CanGoNext { get; protected set; }
    public override string? CustomNextStepName { get; protected set; }

    public override bool HasPreviousStep { get; protected set; }
    public override bool CanGoBack { get; protected set; }

    public override string? CustomCloseWizardCommandName { get; protected set; }

    public ulong PostOwnerId { get; }
    public ulong PostId { get; }

    public IReadOnlyList<IVkPostAttachmentVM> Attachments { get; }

    [OnChangedMethod(nameof(OnSelectedAttachmentChanged))]
    public IVkPostAttachmentVM? SelectedAttachment { get; set; }

    public SelectVkPostAttachmentStepVM(
        ulong postOwnerId,
        ulong postId,
        IReadOnlyList<IVkPostAttachmentVM> attachments)
    {
        PostOwnerId = postOwnerId;
        PostId = postId;
        Attachments = attachments;

        CanSafelyCloseWizard = false;
        HasNextStep = true;
        CanGoNext = false;
        CustomNextStepName = null;
        HasPreviousStep = true;
        CanGoBack = true;
        CustomCloseWizardCommandName = null;
    }

    protected override IWizardStepVM GetNextStep()
    {
        throw new NotImplementedException();
    }

    protected override IWizardStepVM GetPreviousStep()
    {
        throw new NotImplementedException();
    }

    private void OnSelectedAttachmentChanged()
    {
        CanGoNext = SelectedAttachment is not null;
    }
}