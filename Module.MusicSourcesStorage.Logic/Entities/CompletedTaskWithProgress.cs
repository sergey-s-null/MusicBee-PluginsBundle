using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class CompletedTaskWithProgress<TResult> :
    IActivableTaskWithProgress<TResult>,
    IActivableWithoutCancellationTaskWithProgress<TResult>
{
    public event EventHandler<ProgressChangedEventArgs>? ProgressChanged;
    public event EventHandler<TaskFailedEventArgs>? Failed;
    public event EventHandler? Cancelled;
    public event EventHandler<TaskResultEventArgs<TResult>>? SuccessfullyCompleted;

    public bool IsActivated => true;

    public Task<TResult> Task { get; }

    public CompletedTaskWithProgress(TResult result)
    {
        Task = System.Threading.Tasks.Task.FromResult(result);
    }

    public void Activate(CancellationToken token)
    {
    }

    public void Activate()
    {
    }
}