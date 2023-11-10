namespace Module.MusicSourcesStorage.Logic.Entities;

// todo move args in separate folder
public sealed class StepCancelledEventArgs : EventArgs
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