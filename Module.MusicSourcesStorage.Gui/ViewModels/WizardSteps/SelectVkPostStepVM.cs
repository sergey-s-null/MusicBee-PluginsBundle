using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Gui.Helpers;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

[AddINotifyPropertyChangedInterface]
public sealed class SelectVkPostStepVM : ISelectVkPostStepVM
{
    public bool IsValidState { get; private set; }

    [OnChangedMethod(nameof(OnPostGlobalIdChanged))]
    public string PostGlobalId { get; set; }

    public bool IsValidPostGlobalId { get; private set; }

    public long? OwnerId { get; private set; }
    public long? PostId { get; private set; }

    private readonly IAddingVkPostWithArchiveContext _context;

    public SelectVkPostStepVM(
        IAddingVkPostWithArchiveContext context,
        IVkService vkService)
    {
        _context = context;

        PostGlobalId = context.PostId is not null
            ? vkService.GetPostGlobalIdString(context.PostId)
            : string.Empty;
    }

    public StepResult Confirm()
    {
        if (!IsValidState || OwnerId is null || PostId is null)
        {
            throw new InvalidOperationException();
        }

        _context.PostId = new VkPostGlobalId(OwnerId.Value, PostId.Value);

        return StepResult.Success;
    }

    private void OnPostGlobalIdChanged()
    {
        if (VkHelper.TryParsePostGlobalId(PostGlobalId, out var postOwnerId, out var postId))
        {
            OwnerId = postOwnerId;
            PostId = postId;
            IsValidPostGlobalId = true;
            IsValidState = true;
        }
        else
        {
            OwnerId = null;
            PostId = null;
            IsValidPostGlobalId = false;
            IsValidState = false;
        }
    }
}