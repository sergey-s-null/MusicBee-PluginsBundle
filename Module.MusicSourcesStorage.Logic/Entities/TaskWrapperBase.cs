using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Entities;

public abstract class TaskWrapperBase<TResult> : ITaskWithProgress<TResult>
{
    public event EventHandler<ProgressChangedEventArgs>? ProgressChanged;
    public event EventHandler<TaskFailedEventArgs>? Failed;
    public event EventHandler? Cancelled;
    public event EventHandler<TaskResultEventArgs<TResult>>? SuccessfullyCompleted;

    public abstract bool IsActivated { get; }

    public abstract Task<TResult> Task { get; }

    protected void InitializeEvents(ITaskWithProgress<TResult> task)
    {
        InitializeCommonEvents(task);
        task.SuccessfullyCompleted += (_, args) => SuccessfullyCompleted?.Invoke(
            this,
            args
        );
    }

    protected void InitializeEventsWithDifferentResult<TDifferentResult>(
        ITaskWithProgress<TDifferentResult> task,
        Func<TDifferentResult, TResult> resultSelector)
    {
        InitializeCommonEvents(task);
        task.SuccessfullyCompleted += (_, args) => SuccessfullyCompleted?.Invoke(
            this,
            new TaskResultEventArgs<TResult>(resultSelector(args.Result))
        );
    }

    private void InitializeCommonEvents<T>(ITaskWithProgress<T> task)
    {
        task.ProgressChanged += (_, args) => ProgressChanged?.Invoke(
            this,
            args
        );
        task.Failed += (_, args) => Failed?.Invoke(
            this,
            args
        );
        task.Cancelled += (_, args) => Cancelled?.Invoke(
            this,
            args
        );
    }
}