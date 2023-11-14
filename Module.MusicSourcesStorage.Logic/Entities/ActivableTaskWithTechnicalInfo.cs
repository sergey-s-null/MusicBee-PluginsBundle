using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;
using Module.MusicSourcesStorage.Logic.Extensions;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class ActivableTaskWithTechnicalInfo<TResult>
{
    public IActivableTask<Void, TResult> ActivableTask { get; }

    public IActivableTaskWithoutCancellation<Void, TResult> TaskWithEmbeddedToken =>
        _taskWithEmbeddedToken.Value;

    public CancellationTokenSource TokenSource { get; }
    public int TaskRequestingCount => _taskRequestingCount;

    private int _taskRequestingCount;
    private readonly Lazy<IActivableTaskWithoutCancellation<Void, TResult>> _taskWithEmbeddedToken;

    public ActivableTaskWithTechnicalInfo(
        IActivableTask<Void, TResult> activableTask,
        CancellationTokenSource tokenSource,
        int taskRequestingInitCount)
    {
        ActivableTask = activableTask;
        TokenSource = tokenSource;
        _taskRequestingCount = taskRequestingInitCount;

        _taskWithEmbeddedToken = new Lazy<IActivableTaskWithoutCancellation<Void, TResult>>(
            () => ActivableTask.WithToken(tokenSource.Token)
        );
    }

    public void IncreaseTaskRequestingCount()
    {
        Interlocked.Increment(ref _taskRequestingCount);
    }

    public void DecreaseTaskRequestingCount()
    {
        Interlocked.Decrement(ref _taskRequestingCount);
    }
}