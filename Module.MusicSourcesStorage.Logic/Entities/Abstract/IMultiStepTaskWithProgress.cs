using Module.MusicSourcesStorage.Logic.Entities.EventArgs;

namespace Module.MusicSourcesStorage.Logic.Entities.Abstract;

public interface IMultiStepTaskWithProgress<TResult>
{
    event EventHandler<StepProgressChangedEventArgs> ProgressChanged;
    event EventHandler<StepFailedEventArgs> Failed;
    event EventHandler<StepCancelledEventArgs> Cancelled;
    event EventHandler<StepSuccessfullyCompletedEventArgs> StepSuccessfullyCompleted;
    event EventHandler<TaskResultEventArgs<TResult>> FullySuccessfullyCompleted;

    int StepCount { get; }

    bool IsActivated { get; }

    /// <exception cref="InvalidOperationException">Task is not activated.</exception>
    Task<TResult> Task { get; }
}