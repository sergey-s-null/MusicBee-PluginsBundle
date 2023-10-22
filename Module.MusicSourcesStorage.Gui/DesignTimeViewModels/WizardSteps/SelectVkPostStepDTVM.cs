using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Gui.Helpers;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.WizardSteps;

[AddINotifyPropertyChangedInterface]
public sealed class SelectVkPostStepDTVM : ISelectVkPostStepVM
{
    public bool IsValidState { get; private set; }

    [OnChangedMethod(nameof(OnGlobalPostIdChanged))]
    public string PostGlobalId { get; set; } = string.Empty;

    public bool IsValidPostGlobalId { get; private set; }

    public ulong? OwnerId { get; private set; }
    public ulong? PostId { get; private set; }

    private void OnGlobalPostIdChanged()
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

    public StepResult Confirm()
    {
        throw new NotImplementedException();
    }
}