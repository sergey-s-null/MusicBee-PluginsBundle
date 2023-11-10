namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class StepProgressChangedEventArgs : ProgressChangedEventArgs
{
    public int StepIndex { get; }

    public StepProgressChangedEventArgs(int stepIndex, double progress)
        : base(progress)
    {
        StepIndex = stepIndex;
    }
}