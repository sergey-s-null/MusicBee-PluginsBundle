namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class StepFailedEventArgs : TaskFailedEventArgs
{
    /// <summary>
    /// Index of step which was failed.
    /// </summary>
    public int StepIndex { get; }

    public StepFailedEventArgs(int stepIndex, Exception exception) : base(exception)
    {
        StepIndex = stepIndex;
    }
}