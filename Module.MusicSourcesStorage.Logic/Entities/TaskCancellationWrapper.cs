using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class TaskCancellationWrapper<T> : ITaskWithProgress<T>
{
    public event EventHandler<ProgressChangedEventArgs>? ProgressChanged;
    public event EventHandler<TaskFailedEventArgs>? Failed;
    public event EventHandler? Cancelled;
    public event EventHandler<TaskResultEventArgs<T>>? SuccessfullyCompleted;

    public Task<T> Task { get; }

    private readonly ITaskWithProgress<T> _internalTask;

    public TaskCancellationWrapper(ITaskWithProgress<T> internalTask, CancellationToken token = default)
    {
        _internalTask = internalTask;

        InitializeEvents();

        Task = new Task<T>(() => _internalTask.Task.Result, token);
        token.Register(() => Cancelled?.Invoke(this, EventArgs.Empty));
    }

    public void Activate()
    {
        _internalTask.Activate();

        if (Task.Status == TaskStatus.Created)
        {
            Task.Start();
        }
    }

    private void InitializeEvents()
    {
        _internalTask.ProgressChanged += (_, args) => ProgressChanged?.Invoke(this, args);
        _internalTask.Failed += (_, args) => Failed?.Invoke(this, args);
        _internalTask.Cancelled += (_, args) => Cancelled?.Invoke(this, args);
        _internalTask.SuccessfullyCompleted += (_, args) => SuccessfullyCompleted?.Invoke(this, args);
    }
}