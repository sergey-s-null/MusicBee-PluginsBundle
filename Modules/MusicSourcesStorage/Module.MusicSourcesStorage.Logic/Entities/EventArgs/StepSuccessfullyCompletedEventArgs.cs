namespace Module.MusicSourcesStorage.Logic.Entities.EventArgs;

public sealed class StepSuccessfullyCompletedEventArgs : System.EventArgs
{
    public int StepIndex { get; }

    public StepSuccessfullyCompletedEventArgs(int stepIndex)
    {
        StepIndex = stepIndex;
    }
}