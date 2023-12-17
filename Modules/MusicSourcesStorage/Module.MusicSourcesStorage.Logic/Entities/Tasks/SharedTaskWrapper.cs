using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;
using Module.MusicSourcesStorage.Logic.Extensions;

namespace Module.MusicSourcesStorage.Logic.Entities.Tasks;

public sealed class SharedTaskWrapper<TArgs, TResult> :
    TaskWrapperBase<TResult>,
    IActivableTask<TArgs, TResult>
{
    public override bool IsActivated => _isActivated;

    public override Task<TResult> Task => IsActivated && _task is not null
        ? _task
        : throw new InvalidOperationException("Task is not activated.");

    private bool _isActivated;
    private Task<TResult>? _task;

    private readonly Func<TArgs, ISharedTask<TResult>> _sharedTaskProvider;

    public SharedTaskWrapper(
        Func<TArgs, ISharedTask<TResult>> sharedTaskProvider)
    {
        _sharedTaskProvider = sharedTaskProvider;
    }

    public void Activate(TArgs args, CancellationToken token)
    {
        if (IsActivated || _task is not null)
        {
            throw new InvalidOperationException("Task already activated.");
        }

        var sharedTask = _sharedTaskProvider(args);
        InitializeEvents(sharedTask);
        sharedTask.Acquire();
        _ = sharedTask.Activated();

        _task = System.Threading.Tasks.Task.Run(
            () => sharedTask.Task.Result,
            token
        );

        token.Register(() =>
        {
            sharedTask.Release();
            DispatchCancelledEvent();
        });

        _isActivated = true;
    }
}