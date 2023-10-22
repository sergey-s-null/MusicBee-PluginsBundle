using AutoMapper;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
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

    private readonly IVkPostWithArchiveMusicSourceBuilder _musicSourceBuilder;

    public SelectDocumentFromVkPostStepVM(
        IReadOnlyList<VkDocument> documents,
        IVkPostWithArchiveMusicSourceBuilder musicSourceBuilder,
        IMapperBase mapper)
    {
        _musicSourceBuilder = musicSourceBuilder;

        ValidateCurrentState();

        CanSafelyCloseWizard = false;
        HasNextStep = true;
        CanGoNext = false;
        CustomNextStepName = null;
        HasPreviousStep = true;
        CanGoBack = true;
        CustomCloseWizardCommandName = null;

        PostOwnerId = _musicSourceBuilder.PostId!.OwnerId;
        PostId = _musicSourceBuilder.PostId!.LocalId;
        Documents = mapper.Map<IReadOnlyList<IVkDocumentVM>>(documents);
        RestoreSelectedDocument();
    }

    protected override IWizardStepVM GetNextStep()
    {
        if (SelectedDocument is null)
        {
            throw new InvalidOperationException();
        }

        _musicSourceBuilder.Document = SelectedDocument.UnderlyingModel;

        throw new NotImplementedException();
    }

    protected override IWizardStepVM GetPreviousStep()
    {
        throw new NotImplementedException();
    }

    private void ValidateCurrentState()
    {
        if (_musicSourceBuilder.PostId is null)
        {
            throw new InvalidOperationException(
                "Music source builder has invalid state. " +
                "PostId is null."
            );
        }
    }

    private void RestoreSelectedDocument()
    {
        if (_musicSourceBuilder.Document is null)
        {
            return;
        }

        SelectedDocument = Documents.FirstOrDefault(
            x => x.UnderlyingModel == _musicSourceBuilder.Document
        );
    }

    private void OnSelectedDocumentChanged()
    {
        CanGoNext = SelectedDocument is not null;
    }
}