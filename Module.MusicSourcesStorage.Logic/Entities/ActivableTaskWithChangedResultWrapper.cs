using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class ActivableTaskWithChangedResultWrapper<TArgs, TInternalResult, TChangedResult> :
    TaskWrapperBase<TChangedResult>,
    IActivableTaskWithProgress<TArgs, TChangedResult>
{
    public override bool IsActivated => _isActivated;

    public override Task<TChangedResult> Task => IsActivated && _task is not null
        ? _task
        : throw new InvalidOperationException("Task is not activated.");

    private bool _isActivated;
    private Task<TChangedResult>? _task;
    private readonly IActivableTaskWithProgress<TArgs, TInternalResult> _internalTask;
    private readonly Func<TArgs, TInternalResult, TChangedResult> _resultSelector;

    public ActivableTaskWithChangedResultWrapper(
        IActivableTaskWithProgress<TArgs, TInternalResult> internalTask,
        Func<TArgs, TInternalResult, TChangedResult> resultSelector)
    {
        _internalTask = internalTask;
        _resultSelector = resultSelector;

        ValidateInternalTaskIsNotActivated();
    }

    public void Activate(TArgs args, CancellationToken token)
    {
        if (IsActivated || _task is not null)
        {
            throw new InvalidOperationException("Task already activated.");
        }

        _task = System.Threading.Tasks.Task.Run(
            () => Execute(args, token),
            token
        );
        _isActivated = true;
    }

    private TChangedResult Execute(TArgs args, CancellationToken token)
    {
        InitializeEventsWithDifferentResult(
            _internalTask,
            internalResult => _resultSelector(args, internalResult)
        );

        _internalTask.Activate(args, token);

        return _resultSelector(args, _internalTask.Task.Result);
    }

    private void ValidateInternalTaskIsNotActivated()
    {
        if (_internalTask.IsActivated)
        {
            throw new InvalidOperationException("Could not wrap task that already activated.");
        }
    }
}