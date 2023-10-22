using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

public sealed class ReceiveVkPostDocumentsStepVM : ProcessingStepBaseVM
{
    public override string Text { get; protected set; }

    private readonly ulong _postOwnerId;
    private readonly ulong _postId;

    private readonly IVkService _vkService;

    public ReceiveVkPostDocumentsStepVM(
        ulong postOwnerId,
        ulong postId,
        IVkService vkService)
    {
        // todo get ids from context
        _postOwnerId = postOwnerId;
        _postId = postId;

        _vkService = vkService;

        Text = "Receiving documents attached to post";
    }

    protected override async Task ProcessAsync(CancellationToken token)
    {
        var documents = await _vkService.GetAttachedDocumentsFromPostAsync(_postOwnerId, _postId, token);

        // todo save docs to context
        throw new NotImplementedException();
    }
}