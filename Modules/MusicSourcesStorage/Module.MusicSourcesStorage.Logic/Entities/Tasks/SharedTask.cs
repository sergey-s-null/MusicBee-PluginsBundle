using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

namespace Module.MusicSourcesStorage.Logic.Entities.Tasks;

public sealed class SharedTask<TResult> : TaskWrapperBase<TResult>, ISharedTask<TResult>
{
    public override bool IsActivated => _isActivated;

    public override Task<TResult> Task => _isActivated
        ? _internalTask.Value.Task
        : throw new InvalidOperationException("Task is not activated.");

    private readonly Lazy<IActivableTask<Void, TResult>> _internalTask;
    private readonly CancellationTokenSource _tokenSource;

    private int _acquiredCount;
    private bool _isActivated;

    public SharedTask(Func<IActivableTask<Void, TResult>> internalTaskFactory)
    {
        _internalTask = new Lazy<IActivableTask<Void, TResult>>(internalTaskFactory);
        _tokenSource = new CancellationTokenSource();
    }

    public void Activate(Void _)
    {
        if (_isActivated)
        {
            throw new InvalidOperationException("Task already activated.");
        }

        InitializeEvents(_internalTask.Value);

        _internalTask.Value.Activate(Void.Instance, _tokenSource.Token);

        _isActivated = true;
    }

    public void Acquire()
    {
        Interlocked.Increment(ref _acquiredCount);
    }

    public void Release()
    {
        if (_acquiredCount == 0)
        {
            throw new InvalidOperationException("Could not release because task was not acquired.");
        }

        Interlocked.Decrement(ref _acquiredCount);

        if (_acquiredCount == 0)
        {
            _tokenSource.Cancel();
        }
    }
}