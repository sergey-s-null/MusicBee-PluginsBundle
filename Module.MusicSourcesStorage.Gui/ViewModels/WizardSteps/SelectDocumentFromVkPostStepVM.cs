using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

[AddINotifyPropertyChangedInterface]
public sealed class SelectDocumentFromVkPostStepVM : ManualStepBaseVM, ISelectDocumentFromVkPostStepVM
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

    public IReadOnlyList<IVkDocumentVM> Documents { get; }

    [OnChangedMethod(nameof(OnSelectedDocumentChanged))]
    public IVkDocumentVM? SelectedDocument { get; set; }

    public SelectDocumentFromVkPostStepVM(
        ulong postOwnerId,
        ulong postId,
        IReadOnlyList<IVkDocumentVM> documents)
    {
        PostOwnerId = postOwnerId;
        PostId = postId;
        Documents = documents;

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

    private void OnSelectedDocumentChanged()
    {
        CanGoNext = SelectedDocument is not null;
    }
}