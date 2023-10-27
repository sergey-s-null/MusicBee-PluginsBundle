using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Extensions;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

[AddINotifyPropertyChangedInterface]
public sealed class AddMusicSourceToDatabaseStepVM : ProcessingStepBaseVM
{
    public override string Text { get; protected set; }

    private readonly IAddingVkPostWithArchiveContext _context;
    private readonly IMusicSourcesStorageService _storageService;

    public AddMusicSourceToDatabaseStepVM(
        IAddingVkPostWithArchiveContext context,
        IMusicSourcesStorageService storageService)
        : base(context)
    {
        _context = context;
        _storageService = storageService;

        ValidateContext();

        Text = "Starting";
    }

    protected override Task ProcessAsync(CancellationToken token)
    {
        Text = "Adding music source to database";

        return _storageService.AddMusicSourceAsync(
            _context.PostId!,
            _context.SelectedDocument!,
            _context.IndexedFiles!,
            token
        );
    }

    private void ValidateContext()
    {
        _context.ValidateHasPostId();
        _context.ValidateHasSelectedDocument();
        _context.ValidateHasIndexedFiles();
    }
}