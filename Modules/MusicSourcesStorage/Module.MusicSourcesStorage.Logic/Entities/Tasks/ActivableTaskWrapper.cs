using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;
using Module.MusicSourcesStorage.Logic.Extensions;

namespace Module.MusicSourcesStorage.Logic.Entities.Tasks;

public sealed class ActivableTaskWrapper<TArgs, TResult> :
    TaskWrapperBase<TResult>,
    IActivableTask<TArgs, TResult>
{
    public override bool IsActivated => _isActivated;

    public override Task<TResult> Task => IsActivated && _task is not null
        ? _task
        : throw new InvalidOperationException("Task is not activated.");

    private bool _isActivated;
    private Task<TResult>? _task;

    private readonly Func<TArgs, CancellationToken, IActivableTaskWithoutCancellation<Void, TResult>>
        _internalTaskProvider;

    public ActivableTaskWrapper(
        Func<TArgs, CancellationToken, IActivableTaskWithoutCancellation<Void, TResult>>
            internalTaskProvider)
    {
        _internalTaskProvider = internalTaskProvider;
    }

    public void Activate(TArgs args, CancellationToken token)
    {
        if (IsActivated || _task is not null)
        {
            throw new InvalidOperationException("Task already activated.");
        }

        var internalTask = _internalTaskProvider(args, token);
        InitializeEvents(internalTask);
        _ = internalTask.Activated();

        _task = System.Threading.Tasks.Task.Run(
            () => internalTask.Task.Result,
            token
        );
        token.Register(DispatchCancelledEvent);
        _isActivated = true;
    }
}