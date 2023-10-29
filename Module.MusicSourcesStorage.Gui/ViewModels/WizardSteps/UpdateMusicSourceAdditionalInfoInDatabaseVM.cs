using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Gui.Extensions;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

public sealed class UpdateMusicSourceAdditionalInfoInDatabaseVM : ProcessingStepBaseVM
{
    public override string Text { get; protected set; }

    private readonly IMusicSourceContext _musicSourceContext;
    private readonly IMusicSourceAdditionalInfoContext _additionalInfoContext;
    private readonly IMusicSourcesStorageService _storageService;

    public UpdateMusicSourceAdditionalInfoInDatabaseVM(
        IMusicSourceContext musicSourceContext,
        IMusicSourceAdditionalInfoContext additionalInfoContext,
        IMusicSourcesStorageService storageService,
        IWizardErrorContext errorContext)
        : base(errorContext)
    {
        _musicSourceContext = musicSourceContext;
        _additionalInfoContext = additionalInfoContext;
        _storageService = storageService;

        ValidateContext();

        Text = "Starting";
    }

    protected override async Task<StepResult> ProcessAsync(CancellationToken token)
    {
        Text = "Updating music source additional info";

        await _storageService.UpdateAdditionalInfoAsync(
            _musicSourceContext.MusicSourceId,
            _additionalInfoContext.AdditionalInfo!,
            token
        );

        return StepResult.Success;
    }

    private void ValidateContext()
    {
        _additionalInfoContext.ValidateHasAdditionalInfo();
    }
}