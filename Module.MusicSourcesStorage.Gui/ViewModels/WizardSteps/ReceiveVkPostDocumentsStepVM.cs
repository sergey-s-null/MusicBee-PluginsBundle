using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Factories.Abstract;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

public sealed class ReceiveVkPostDocumentsStepVM : ProcessingStepBaseVM
{
    public override bool CanSafelyCloseWizard { get; protected set; }

    public override string Text { get; protected set; }

    private readonly ulong _postOwnerId;
    private readonly ulong _postId;

    private readonly IVkService _vkService;

    public ReceiveVkPostDocumentsStepVM(
        ulong postOwnerId,
        ulong postId,
        IVkService vkService,
        IWizardCommonStepsFactory commonStepsFactory)
        : base(commonStepsFactory)
    {
        _postOwnerId = postOwnerId;
        _postId = postId;

        _vkService = vkService;

        CanSafelyCloseWizard = false;
        Text = "Receiving documents attached to post";
    }

    protected override async Task<IWizardStepVM> ProcessAsync(CancellationToken token)
    {
        var documents = await _vkService.GetAttachedDocumentsFromPostAsync(_postOwnerId, _postId, token);

        throw new NotImplementedException();
    }
}