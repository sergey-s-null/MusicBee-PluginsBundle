using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Gui.Extensions;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

[AddINotifyPropertyChangedInterface]
public sealed class DownloadAndIndexArchiveStepVM : ProcessingStepBaseVM
{
    public override string Text { get; protected set; }

    private readonly IAddingVkPostWithArchiveContext _context;
    private readonly IVkDocumentDownloader _vkDocumentDownloader;
    private readonly IArchiveIndexer _archiveIndexer;

    public DownloadAndIndexArchiveStepVM(
        IAddingVkPostWithArchiveContext context,
        IVkDocumentDownloader vkDocumentDownloader,
        IArchiveIndexer archiveIndexer
    )
        : base(context)
    {
        _context = context;
        _vkDocumentDownloader = vkDocumentDownloader;
        _archiveIndexer = archiveIndexer;

        _context.ValidateHasSelectedDocument();

        Text = "Starting";
    }

    protected override async Task<StepResult> ProcessAsync(CancellationToken token)
    {
        Text = "Downloading archive";
        var archiveFilePath = await _vkDocumentDownloader.DownloadAsync(_context.SelectedDocument!, token);

        Text = "Indexing archive";
        var files = _archiveIndexer.Index(archiveFilePath);

        _context.IndexedFiles = files;

        return StepResult.Success;
    }
}