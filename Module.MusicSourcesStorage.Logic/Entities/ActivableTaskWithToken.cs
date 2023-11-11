using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class ActivableTaskWithToken<TResult> : IActivableWithoutCancellationTaskWithProgress<TResult>
{
    public event EventHandler<ProgressChangedEventArgs>? ProgressChanged;
    public event EventHandler<TaskFailedEventArgs>? Failed;
    public event EventHandler? Cancelled;
    public event EventHandler<TaskResultEventArgs<TResult>>? SuccessfullyCompleted;

    public bool IsActivated => _internalTask.IsActivated;

    public Task<TResult> Task => _internalTask.Task;

    private readonly IActivableTaskWithProgress<TResult> _internalTask;
    private readonly CancellationToken _token;

    public ActivableTaskWithToken(
        IActivableTaskWithProgress<TResult> internalTask,
        CancellationToken token)
    {
        _internalTask = internalTask;
        _token = token;

        InitializeEvents();
    }

    public void Activate()
    {
        _internalTask.Activate(_token);
    }

    private void InitializeEvents()
    {
        _internalTask.ProgressChanged += ProgressChanged;
        _internalTask.Failed += Failed;
        _internalTask.Cancelled += Cancelled;
        _internalTask.SuccessfullyCompleted += SuccessfullyCompleted;
    }
}