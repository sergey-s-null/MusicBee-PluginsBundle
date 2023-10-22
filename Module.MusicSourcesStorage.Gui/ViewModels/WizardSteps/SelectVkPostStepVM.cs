using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Helpers;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

[AddINotifyPropertyChangedInterface]
public sealed class SelectVkPostStepVM : ManualStepBaseVM, ISelectVkPostStepVM
{
    public override bool CanSafelyCloseWizard { get; protected set; }

    public override bool HasNextStep { get; protected set; }
    public override bool CanGoNext { get; protected set; }
    public override string? CustomNextStepName { get; protected set; }

    public override bool HasPreviousStep { get; protected set; }
    public override bool CanGoBack { get; protected set; }

    public override string? CustomCloseWizardCommandName { get; protected set; }

    [OnChangedMethod(nameof(OnPostGlobalIdChanged))]
    public string PostGlobalId { get; set; }

    public bool IsValidPostGlobalId { get; private set; }

    public ulong? OwnerId { get; private set; }
    public ulong? PostId { get; private set; }

    private readonly IVkPostWithArchiveMusicSourceBuilder _musicSourceBuilder;

    public SelectVkPostStepVM(IVkPostWithArchiveMusicSourceBuilder builder)
    {
        _musicSourceBuilder = builder;

        CanSafelyCloseWizard = true;
        HasNextStep = true;
        CanGoNext = false;
        CustomNextStepName = null;
        HasPreviousStep = false;
        CanGoBack = false;
        CustomCloseWizardCommandName = null;

        PostGlobalId = string.Empty;
    }

    protected override IWizardStepVM GetNextStep()
    {
        if (OwnerId is null || PostId is null)
        {
            throw new InvalidOperationException();
        }

        _musicSourceBuilder.PostOwnerId = OwnerId;
        _musicSourceBuilder.PostId = PostId;

        // todo add transition
        throw new NotImplementedException();
    }

    protected override IWizardStepVM GetPreviousStep()
    {
        throw new InvalidOperationException();
    }

    private void OnPostGlobalIdChanged()
    {
        if (VkHelper.TryParsePostGlobalId(PostGlobalId, out var postOwnerId, out var postId))
        {
            OwnerId = postOwnerId;
            PostId = postId;
            IsValidPostGlobalId = true;
            CanGoNext = true;
        }
        else
        {
            OwnerId = null;
            PostId = null;
            IsValidPostGlobalId = false;
            CanGoNext = false;
        }
    }
}