using Module.MusicSourcesStorage.Gui.Enums;

namespace Module.MusicSourcesStorage.Gui.Entities;

public sealed class StepResultEventArgs : EventArgs
{
    public StepResult Result { get; }

    public StepResultEventArgs(StepResult result)
    {
        Result = result;
    }
}