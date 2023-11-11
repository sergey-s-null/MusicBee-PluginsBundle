using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Entities;

public abstract class TaskWithProgressBase<TResult> : ITaskWithProgress<TResult>
{
    public event EventHandler<ProgressChangedEventArgs>? ProgressChanged;
    public event EventHandler<TaskFailedEventArgs>? Failed;
    public event EventHandler? Cancelled;
    public event EventHandler<TaskResultEventArgs<TResult>>? SuccessfullyCompleted;

    public abstract bool IsActivated { get; }

    public abstract Task<TResult> Task { get; }

    protected void OnTaskEnded(Task<TResult> task)
    {
        switch (task)
        {
            case { Status: TaskStatus.RanToCompletion }:
                SuccessfullyCompleted?.Invoke(this, new TaskResultEventArgs<TResult>(task.Result));
                break;
            case { Status: TaskStatus.Canceled }:
                Cancelled?.Invoke(this, EventArgs.Empty);
                break;
            case { Exception: not null }:
                Failed?.Invoke(this, new TaskFailedEventArgs(task.Exception));
                break;
            default:
                Failed?.Invoke(this, new TaskFailedEventArgs(new Exception(
                    $"Task finished without exception with status {task.Status}."
                )));
                break;
        }
    }

    protected void OnProgressChanged(double progress)
    {
        ProgressChanged?.Invoke(
            this,
            new ProgressChangedEventArgs(progress)
        );
    }
}