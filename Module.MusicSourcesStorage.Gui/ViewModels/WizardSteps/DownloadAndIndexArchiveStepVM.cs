using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

public sealed class DownloadAndIndexArchiveStepVM : ProcessingStepBaseVM
{
    public override string Text { get; protected set; }

    private readonly VkDocument _vkDocument;

    private readonly IVkDocumentDownloader _vkDocumentDownloader;
    private readonly IArchiveIndexer _archiveIndexer;

    public DownloadAndIndexArchiveStepVM(
        VkDocument vkDocument,
        IVkDocumentDownloader vkDocumentDownloader,
        IArchiveIndexer archiveIndexer)
    {
        _vkDocument = vkDocument;
        _vkDocumentDownloader = vkDocumentDownloader;
        _archiveIndexer = archiveIndexer;

        Text = "Starting";
    }

    protected override async Task ProcessAsync(CancellationToken token)
    {
        Text = "Downloading archive";
        var archiveFilePath = await _vkDocumentDownloader.DownloadAsync(_vkDocument, token);

        Text = "Indexing archive";
        var files = _archiveIndexer.Index(archiveFilePath);

        // todo save result to context
        throw new NotImplementedException();
    }
}