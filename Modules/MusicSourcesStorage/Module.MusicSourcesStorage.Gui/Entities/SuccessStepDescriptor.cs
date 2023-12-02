using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.Entities;

public sealed class SuccessStepDescriptor : WizardStepDescriptor, ISuccessStepDescriptor
{
    public string Text { get; }

    public SuccessStepDescriptor() : this("Success")
    {
    }

    public SuccessStepDescriptor(string text) : base(StepType.SuccessResult)
    {
        Text = text;
    }
}