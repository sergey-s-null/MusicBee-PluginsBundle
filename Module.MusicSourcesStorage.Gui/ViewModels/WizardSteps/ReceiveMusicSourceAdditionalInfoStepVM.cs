using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

public sealed class ReceiveMusicSourceAdditionalInfoStepVM : ProcessingStepBaseVM
{
    public override string Text { get; protected set; }

    private readonly IMusicSourceContext _musicSourceContext;
    private readonly IWizardErrorContext _errorContext;
    private readonly IMusicSourceAdditionalInfoContext _additionalInfoContext;
    private readonly IMusicSourcesStorageService _storageService;

    public ReceiveMusicSourceAdditionalInfoStepVM(
        IMusicSourceContext musicSourceContext,
        IWizardErrorContext errorContext,
        IMusicSourceAdditionalInfoContext additionalInfoContext,
        IMusicSourcesStorageService storageService) : base(errorContext)
    {
        _musicSourceContext = musicSourceContext;
        _errorContext = errorContext;
        _additionalInfoContext = additionalInfoContext;
        _storageService = storageService;

        Text = "Starting";
    }

    protected override async Task<StepResult> ProcessAsync(CancellationToken token)
    {
        Text = "Receiving additional info from database";

        var additionalInfo = await _storageService.GetAdditionalInfoByIdAsync(_musicSourceContext.MusicSourceId, token);
        if (additionalInfo is null)
        {
            _errorContext.Error =
                $"Could not get additional info from database for music source with id {_musicSourceContext.MusicSourceId}.";
            return StepResult.Error;
        }

        _additionalInfoContext.AdditionalInfo = additionalInfo;

        return StepResult.Success;
    }
}