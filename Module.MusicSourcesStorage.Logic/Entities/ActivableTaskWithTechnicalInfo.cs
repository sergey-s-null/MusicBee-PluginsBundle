using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;
using Module.MusicSourcesStorage.Logic.Extensions;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class ActivableTaskWithTechnicalInfo<TResult>
{
    public IActivableTaskWithProgress<Void, TResult> TaskWithProgress { get; }

    public IActivableWithoutCancellationTaskWithProgress<Void, TResult> TaskWithEmbeddedToken =>
        _taskWithEmbeddedToken.Value;

    public CancellationTokenSource TokenSource { get; }
    public int TaskRequestingCount => _taskRequestingCount;

    private int _taskRequestingCount;
    private readonly Lazy<IActivableWithoutCancellationTaskWithProgress<Void, TResult>> _taskWithEmbeddedToken;

    public ActivableTaskWithTechnicalInfo(
        IActivableTaskWithProgress<Void, TResult> taskWithProgress,
        CancellationTokenSource tokenSource,
        int taskRequestingInitCount)
    {
        TaskWithProgress = taskWithProgress;
        TokenSource = tokenSource;
        _taskRequestingCount = taskRequestingInitCount;

        _taskWithEmbeddedToken = new Lazy<IActivableWithoutCancellationTaskWithProgress<Void, TResult>>(
            () => TaskWithProgress.WithToken(tokenSource.Token)
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