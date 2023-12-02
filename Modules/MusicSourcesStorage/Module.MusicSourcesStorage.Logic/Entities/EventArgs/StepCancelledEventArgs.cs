namespace Module.MusicSourcesStorage.Logic.Entities.EventArgs;

public sealed class StepCancelledEventArgs : System.EventArgs
{
    /// <summary>
    /// Index of step which was cancelled.
    /// </summary>
    public int StepIndex { get; }

    public StepCancelledEventArgs(int stepIndex)
    {
        StepIndex = stepIndex;
    }
}