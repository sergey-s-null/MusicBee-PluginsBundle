using Module.MusicSourcesStorage.Logic.Delegates;
using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class DefaultTaskWithProgress<T> : ITaskWithProgress<T>
{
    public event EventHandler<ProgressChangedEventArgs>? ProgressChanged;
    public event EventHandler<TaskFailedEventArgs>? Failed;
    public event EventHandler? Cancelled;
    public event EventHandler<TaskResultEventArgs<T>>? SuccessfullyCompleted;

    public Task<T> Task { get; }

    public DefaultTaskWithProgress(TaskFunction<T> taskFunction, CancellationToken token)
    {
        Task = new Task<T>(() => taskFunction(OnProgressChanged, token), token);
        Task.ContinueWith(OnTaskEnded);
    }

    public void Activate()
    {
        if (Task.Status != TaskStatus.Created)
        {
            return;
        }

        Task.Start();
    }

    private void OnTaskEnded(Task<T> task)
    {
        switch (task)
        {
            case { Status: TaskStatus.RanToCompletion }:
                SuccessfullyCompleted?.Invoke(this, new TaskResultEventArgs<T>(task.Result));
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

    private void OnProgressChanged(double progress)
    {
        ProgressChanged?.Invoke(
            this,
            new ProgressChangedEventArgs(progress)
        );
    }
}