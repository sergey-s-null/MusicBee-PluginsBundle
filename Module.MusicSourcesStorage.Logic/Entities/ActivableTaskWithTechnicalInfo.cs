using Module.MusicSourcesStorage.Logic.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Extensions;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class ActivableTaskWithTechnicalInfo<TResult>
{
    public IActivableTaskWithProgress<TResult> TaskWithProgress { get; }
    public IActivableWithoutCancellationTaskWithProgress<TResult> TaskWithEmbeddedToken => _taskWithEmbeddedToken.Value;
    public CancellationTokenSource TokenSource { get; }
    public int TaskRequestingCount => _taskRequestingCount;

    private int _taskRequestingCount;
    private readonly Lazy<IActivableWithoutCancellationTaskWithProgress<TResult>> _taskWithEmbeddedToken;

    public ActivableTaskWithTechnicalInfo(
        IActivableTaskWithProgress<TResult> taskWithProgress,
        CancellationTokenSource tokenSource,
        int taskRequestingInitCount)
    {
        TaskWithProgress = taskWithProgress;
        TokenSource = tokenSource;
        _taskRequestingCount = taskRequestingInitCount;

        _taskWithEmbeddedToken = new Lazy<IActivableWithoutCancellationTaskWithProgress<TResult>>(
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