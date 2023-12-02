using Module.MusicSourcesStorage.Logic.Entities.EventArgs;

namespace Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

public abstract class TaskWrapperBase<TResult> : ITask<TResult>
{
    public event EventHandler<ProgressChangedEventArgs>? ProgressChanged;
    public event EventHandler<TaskFailedEventArgs>? Failed;
    public event EventHandler? Cancelled;
    public event EventHandler<TaskResultEventArgs<TResult>>? SuccessfullyCompleted;

    public abstract bool IsActivated { get; }

    public abstract Task<TResult> Task { get; }

    protected void DispatchCancelledEvent()
    {
        Cancelled?.Invoke(this, System.EventArgs.Empty);
    }

    protected void InitializeEvents(ITask<TResult> task)
    {
        InitializeCommonEvents(task);
        task.SuccessfullyCompleted += (_, args) => SuccessfullyCompleted?.Invoke(
            this,
            args
        );
    }

    protected void InitializeEventsWithDifferentResult<TDifferentResult>(
        ITask<TDifferentResult> task,
        Func<TDifferentResult, TResult> resultSelector)
    {
        InitializeCommonEvents(task);
        task.SuccessfullyCompleted += (_, args) => SuccessfullyCompleted?.Invoke(
            this,
            new TaskResultEventArgs<TResult>(resultSelector(args.Result))
        );
    }

    private void InitializeCommonEvents<T>(ITask<T> task)
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