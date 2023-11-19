using Module.MusicSourcesStorage.Gui.AbstractViewModels;
using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;
using Module.MusicSourcesStorage.Gui.Extensions;
using Module.MusicSourcesStorage.Gui.Factories;
using Module.MusicSourcesStorage.Logic.Extensions;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

[AddINotifyPropertyChangedInterface]
public sealed class IndexTorrentStepVM : ProcessingStepBaseVM
{
    public override string Text { get; protected set; }

    public override IProgressVM? Progress { get; protected set; }

    private readonly ITorrentFileContext _torrentFileContext;
    private readonly IIndexedFilesContext _indexedFilesContext;
    private readonly ITorrentIndexer _torrentIndexer;

    public IndexTorrentStepVM(
        ITorrentFileContext torrentFileContext,
        IIndexedFilesContext indexedFilesContext,
        ITorrentIndexer torrentIndexer,
        IWizardErrorContext errorContext)
        : base(errorContext)
    {
        _torrentFileContext = torrentFileContext;
        _indexedFilesContext = indexedFilesContext;
        _torrentIndexer = torrentIndexer;

        ValidateContext();

        Text = "Starting";
    }

    protected override async Task<StepResult> ProcessAsync(CancellationToken token)
    {
        Text = "Indexing torrent file";

        var task = _torrentIndexer.CreateIndexingTask();
        Progress = ProgressVMFactory.Create(task);

        var files = await task
            .Activated(_torrentFileContext.TorrentFilePath!, token)
            .Task;

        _indexedFilesContext.IndexedFiles = files;

        return StepResult.Success;
    }

    private void ValidateContext()
    {
        _torrentFileContext.ValidateHasTorrentFilePath();
    }
}