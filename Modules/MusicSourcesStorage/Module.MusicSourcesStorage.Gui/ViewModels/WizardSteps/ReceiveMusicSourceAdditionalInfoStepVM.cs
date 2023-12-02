using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

public sealed class ReceiveMusicSourceAdditionalInfoStepVM : ProcessingStepBaseVM
{
    public override string Text { get; protected set; }
    public override IProgressVM? Progress { get; protected set; }

    private readonly IMusicSourceContext _musicSourceContext;
    private readonly IWizardErrorContext _errorContext;
    private readonly IInitialMusicSourceAdditionalInfoContext _initialAdditionalInfoContext;
    private readonly IMusicSourcesStorageService _storageService;

    public ReceiveMusicSourceAdditionalInfoStepVM(
        IMusicSourceContext musicSourceContext,
        IWizardErrorContext errorContext,
        IInitialMusicSourceAdditionalInfoContext initialAdditionalInfoContext,
        IMusicSourcesStorageService storageService) : base(errorContext)
    {
        _musicSourceContext = musicSourceContext;
        _errorContext = errorContext;
        _initialAdditionalInfoContext = initialAdditionalInfoContext;
        _storageService = storageService;

        Text = "Starting";
    }

    protected override async Task<StepResult> ProcessAsync(CancellationToken token)
    {
        Text = "Receiving additional info from database";

        var additionalInfo =
            await _storageService.FindAdditionalInfoByIdAsync(_musicSourceContext.MusicSourceId, token);
        if (additionalInfo is null)
        {
            _errorContext.Error =
                $"Could not get additional info from database for music source with id {_musicSourceContext.MusicSourceId}.";
            return StepResult.Error;
        }

        _initialAdditionalInfoContext.InitialAdditionalInfo = additionalInfo;

        return StepResult.Success;
    }
}