namespace Module.MusicSourcesStorage.Logic.Entities.Abstract;

public interface IMultiStepTaskWithProgress<T>
{
    event EventHandler<StepProgressChangedEventArgs> ProgressChanged;
    event EventHandler<StepFailedEventArgs> Failed;
    event EventHandler<StepCancelledEventArgs> Cancelled;
    event EventHandler<StepSuccessfullyCompletedEventArgs> StepSuccessfullyCompleted;
    event EventHandler<TaskResultEventArgs<T>> FullySuccessfullyCompleted;

    int StepCount { get; }

    Task<T> Task { get; }

    void Activate();
}