using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

[AddINotifyPropertyChangedInterface]
public sealed class AddMusicSourceToDatabaseStepVM : ProcessingStepBaseVM
{
    public override string Text { get; protected set; }

    private readonly IAddingVkPostWithArchiveContext _context;

    public AddMusicSourceToDatabaseStepVM(IAddingVkPostWithArchiveContext context)
        : base(context)
    {
        _context = context;
        Text = "Starting";
    }

    protected override Task ProcessAsync(CancellationToken token)
    {
        _context.SuccessResultText = "Music source added to database";
        // todo add to database
        return Task.CompletedTask;
    }
}