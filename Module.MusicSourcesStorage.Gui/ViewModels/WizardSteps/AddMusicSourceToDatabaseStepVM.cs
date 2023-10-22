namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

public sealed class AddMusicSourceToDatabaseStepVM : ProcessingStepBaseVM
{
    public override string Text { get; protected set; }

    public AddMusicSourceToDatabaseStepVM()
    {
        Text = "Starting";
    }

    protected override Task ProcessAsync(CancellationToken token)
    {
        throw new NotImplementedException();
    }
}