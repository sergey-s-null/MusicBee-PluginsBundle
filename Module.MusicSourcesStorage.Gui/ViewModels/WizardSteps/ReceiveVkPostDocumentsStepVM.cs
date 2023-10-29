using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Gui.Extensions;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

[AddINotifyPropertyChangedInterface]
public sealed class ReceiveVkPostDocumentsStepVM : ProcessingStepBaseVM
{
    public override string Text { get; protected set; }

    private readonly long _postOwnerId;
    private readonly long _postId;

    private readonly IAddingVkPostWithArchiveContext _context;
    private readonly IVkService _vkService;

    public ReceiveVkPostDocumentsStepVM(
        IAddingVkPostWithArchiveContext context,
        IVkService vkService
    )
        : base(context)
    {
        _context = context;
        _vkService = vkService;

        _context.ValidateHasPostId();

        _postOwnerId = _context.PostId!.OwnerId;
        _postId = _context.PostId.LocalId;

        Text = "Receiving documents attached to post";
    }

    protected override async Task<StepResult> ProcessAsync(CancellationToken token)
    {
        var documents = await _vkService.GetAttachedDocumentsFromPostAsync(
            _postOwnerId,
            _postId,
            token
        );

        _context.AttachedDocuments = documents;

        return StepResult.Success;
    }
}