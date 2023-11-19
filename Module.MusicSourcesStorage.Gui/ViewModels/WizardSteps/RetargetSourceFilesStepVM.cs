using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Gui.Extensions;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

public sealed class RetargetSourceFilesStepVM : ProcessingStepBaseVM
{
    public override string Text { get; protected set; }

    public override IProgressVM? Progress { get; protected set; }

    private readonly IMusicSourceContext _musicSourceContext;
    private readonly IEditMusicSourceAdditionalInfoContext _additionalInfoContext;
    private readonly ISourceFilesRetargetingService _sourceFilesRetargetingService;

    public RetargetSourceFilesStepVM(
        IMusicSourceContext musicSourceContext,
        IEditMusicSourceAdditionalInfoContext additionalInfoContext,
        ISourceFilesRetargetingService sourceFilesRetargetingService,
        IWizardErrorContext errorContext)
        : base(errorContext)
    {
        _musicSourceContext = musicSourceContext;
        _additionalInfoContext = additionalInfoContext;
        _sourceFilesRetargetingService = sourceFilesRetargetingService;

        ValidateContext();

        Text = "Starting";
    }

    protected override async Task<StepResult> ProcessAsync(CancellationToken token)
    {
        Text = "Retarget source files";
        // todo use task with progress
        await _sourceFilesRetargetingService.RetargetAsync(
            _musicSourceContext.MusicSourceId,
            _additionalInfoContext.InitialAdditionalInfo!,
            _additionalInfoContext.EditedAdditionalInfo!,
            token
        );

        Text = "Files retargeted";
        return StepResult.Success;
    }

    private void ValidateContext()
    {
        _additionalInfoContext.ValidateHasInitialAdditionalInfo();
        _additionalInfoContext.ValidateHasEditedAdditionalInfo();
    }
}