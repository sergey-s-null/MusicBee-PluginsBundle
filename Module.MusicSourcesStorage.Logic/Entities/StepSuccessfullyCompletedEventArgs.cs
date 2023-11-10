namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class StepSuccessfullyCompletedEventArgs : EventArgs
{
    public int StepIndex { get; }

    public StepSuccessfullyCompletedEventArgs(int stepIndex)
    {
        StepIndex = stepIndex;
    }
}