using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class ActivableTaskWithArgs<TArgs, TResult> : IActivableTaskWithProgress<TResult>
{
    public event EventHandler<ProgressChangedEventArgs>? ProgressChanged;
    public event EventHandler<TaskFailedEventArgs>? Failed;
    public event EventHandler? Cancelled;
    public event EventHandler<TaskResultEventArgs<TResult>>? SuccessfullyCompleted;

    public bool IsActivated => _internalTask.IsActivated;

    public Task<TResult> Task => _internalTask.Task;

    private readonly IActivableTaskWithProgress<TArgs, TResult> _internalTask;
    private readonly TArgs _args;

    public ActivableTaskWithArgs(
        IActivableTaskWithProgress<TArgs, TResult> internalTask,
        TArgs args)
    {
        _internalTask = internalTask;
        _args = args;

        InitializeEvents();
    }

    public void Activate(CancellationToken token)
    {
        _internalTask.Activate(_args, token);
    }

    private void InitializeEvents()
    {
        _internalTask.ProgressChanged += ProgressChanged;
        _internalTask.Failed += Failed;
        _internalTask.Cancelled += Cancelled;
        _internalTask.SuccessfullyCompleted += SuccessfullyCompleted;
    }
}