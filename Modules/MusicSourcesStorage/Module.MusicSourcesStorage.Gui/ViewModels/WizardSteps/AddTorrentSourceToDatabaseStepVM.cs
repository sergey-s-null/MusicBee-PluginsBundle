using System.IO;
using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Gui.Extensions;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

[AddINotifyPropertyChangedInterface]
public sealed class AddTorrentSourceToDatabaseStepVM : ProcessingStepBaseVM
{
    public override string Text { get; protected set; }

    public override IProgressVM? Progress { get; protected set; }

    private readonly ITorrentFileContext _torrentFileContext;
    private readonly IIndexedFilesContext _indexedFilesContext;
    private readonly IEditMusicSourceAdditionalInfoContext _additionalInfoContext;
    private readonly IWizardResultContext<MusicSource> _resultContext;
    private readonly IMusicSourcesStorageService _storageService;

    public AddTorrentSourceToDatabaseStepVM(
        ITorrentFileContext torrentFileContext,
        IIndexedFilesContext indexedFilesContext,
        IEditMusicSourceAdditionalInfoContext additionalInfoContext,
        IWizardResultContext<MusicSource> resultContext,
        IMusicSourcesStorageService storageService,
        IWizardErrorContext errorContext)
        : base(errorContext)
    {
        _torrentFileContext = torrentFileContext;
        _indexedFilesContext = indexedFilesContext;
        _additionalInfoContext = additionalInfoContext;
        _resultContext = resultContext;
        _storageService = storageService;

        ValidateContext();

        Text = "Starting";
    }

    protected override async Task<StepResult> ProcessAsync(CancellationToken token)
    {
        Text = "Add torrent source to database";

        var torrentBytes = File.ReadAllBytes(_torrentFileContext.TorrentFilePath!);
        var source = TorrentSource.New(
            _additionalInfoContext.EditedAdditionalInfo!,
            _indexedFilesContext.IndexedFiles!,
            torrentBytes
        );

        _resultContext.Result = await _storageService.AddMusicSourceAsync(source, token);

        return StepResult.Success;
    }

    private void ValidateContext()
    {
        _torrentFileContext.ValidateHasTorrentFilePath();
        _additionalInfoContext.ValidateHasEditedAdditionalInfo();
        _indexedFilesContext.ValidateHasIndexedFiles();
    }
}