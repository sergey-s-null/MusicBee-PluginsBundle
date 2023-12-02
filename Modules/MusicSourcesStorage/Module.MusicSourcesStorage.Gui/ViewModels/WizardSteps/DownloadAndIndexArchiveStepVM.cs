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
public sealed class DownloadAndIndexArchiveStepVM : ProcessingStepBaseVM
{
    public override string Text { get; protected set; }
    public override IProgressVM? Progress { get; protected set; }

    private readonly IAddingVkPostWithArchiveContext _context;
    private readonly IIndexedFilesContext _indexedFilesContext;
    private readonly IVkDocumentDownloadingTaskManager _vkDocumentDownloadingTaskManager;
    private readonly IArchiveIndexer _archiveIndexer;

    public DownloadAndIndexArchiveStepVM(
        IAddingVkPostWithArchiveContext context,
        IIndexedFilesContext indexedFilesContext,
        IVkDocumentDownloadingTaskManager vkDocumentDownloadingTaskManager,
        IArchiveIndexer archiveIndexer)
        : base(context)
    {
        _context = context;
        _indexedFilesContext = indexedFilesContext;
        _vkDocumentDownloadingTaskManager = vkDocumentDownloadingTaskManager;
        _archiveIndexer = archiveIndexer;

        _context.ValidateHasSelectedDocument();

        Text = "Starting";
    }

    protected override async Task<StepResult> ProcessAsync(CancellationToken token)
    {
        Text = "Downloading and indexing archive";

        var task = _vkDocumentDownloadingTaskManager
            .CreateDownloadTask()
            .Chain(_archiveIndexer.CreateIndexingTask());

        Progress = ProgressVMFactory.Create(task);

        var files = await task
            .Activated(_context.SelectedDocument!, token)
            .Task;

        _indexedFilesContext.IndexedFiles = files;

        return StepResult.Success;
    }
}