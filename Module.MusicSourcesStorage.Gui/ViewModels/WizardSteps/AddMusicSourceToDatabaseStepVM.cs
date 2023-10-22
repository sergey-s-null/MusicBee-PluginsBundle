using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using PropertyChanged;

namespace Module.MusicSourcesStorage.Gui.ViewModels.WizardSteps;

[AddINotifyPropertyChangedInterface]
public sealed class AddMusicSourceToDatabaseStepVM : ProcessingStepBaseVM
{
    public override string Text { get; protected set; }

    public AddMusicSourceToDatabaseStepVM(IAddingVkPostWithArchiveContext context)
        : base(context)
    {
        Text = "Starting";
    }

    protected override Task ProcessAsync(CancellationToken token)
    {
        throw new NotImplementedException();
    }
}