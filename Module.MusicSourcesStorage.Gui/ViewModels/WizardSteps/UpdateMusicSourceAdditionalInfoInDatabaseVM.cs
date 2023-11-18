using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Gui.Extensions;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

public sealed class UpdateMusicSourceAdditionalInfoInDatabaseVM : ProcessingStepBaseVM
{
    public override string Text { get; protected set; }
    public override IProgressVM? Progress { get; protected set; }

    private readonly IMusicSourceContext _musicSourceContext;
    private readonly IMusicSourceAdditionalInfoContext _additionalInfoContext;
    private readonly IMusicSourcesStorageService _storageService;
    private readonly IWizardResultContext<MusicSourceAdditionalInfo> _resultContext;

    public UpdateMusicSourceAdditionalInfoInDatabaseVM(
        IMusicSourceContext musicSourceContext,
        IMusicSourceAdditionalInfoContext additionalInfoContext,
        IMusicSourcesStorageService storageService,
        IWizardResultContext<MusicSourceAdditionalInfo> resultContext,
        IWizardErrorContext errorContext)
        : base(errorContext)
    {
        _musicSourceContext = musicSourceContext;
        _additionalInfoContext = additionalInfoContext;
        _storageService = storageService;
        _resultContext = resultContext;

        ValidateContext();

        Text = "Starting";
    }

    protected override async Task<StepResult> ProcessAsync(CancellationToken token)
    {
        Text = "Updating music source additional info";

        var result = await _storageService.UpdateAdditionalInfoAsync(
            _musicSourceContext.MusicSourceId,
            _additionalInfoContext.AdditionalInfo!,
            token
        );

        _resultContext.Result = result;

        return StepResult.Success;
    }

    private void ValidateContext()
    {
        _additionalInfoContext.ValidateHasAdditionalInfo();
    }
}