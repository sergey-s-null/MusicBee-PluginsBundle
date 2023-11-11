using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class ChainedActivableMultiStepTasks<TFirstArgs, TFirstResult, TSecondArgs, TSecondResult, TResult> :
    MultiStepTaskWrapperBase<TResult>,
    IActivableMultiStepTaskWithProgress<TFirstArgs, TResult>
{
    public override int StepCount => _firstTask.StepCount + _secondTask.StepCount;

    public override bool IsActivated => _isActivated;

    public override Task<TResult> Task => IsActivated && _task is not null
        ? _task
        : throw new InvalidOperationException("Task is not started.");

    private bool _isActivated;
    private Task<TResult>? _task;
    private readonly IActivableMultiStepTaskWithProgress<TFirstArgs, TFirstResult> _firstTask;
    private readonly Func<TFirstArgs, TFirstResult, TSecondArgs> _secondArgsSelector;
    private readonly IActivableMultiStepTaskWithProgress<TSecondArgs, TSecondResult> _secondTask;
    private readonly Func<TFirstArgs, TFirstResult, TSecondResult, TResult> _resultSelector;

    public ChainedActivableMultiStepTasks(
        IActivableMultiStepTaskWithProgress<TFirstArgs, TFirstResult> firstTask,
        Func<TFirstArgs, TFirstResult, TSecondArgs> secondArgsSelector,
        IActivableMultiStepTaskWithProgress<TSecondArgs, TSecondResult> secondTask,
        Func<TFirstArgs, TFirstResult, TSecondResult, TResult> resultSelector)
    {
        _firstTask = firstTask;
        _secondArgsSelector = secondArgsSelector;
        _secondTask = secondTask;
        _resultSelector = resultSelector;

        ValidateInternalTasksNotActivated();
    }

    public void Activate(TFirstArgs args, CancellationToken token)
    {
        if (IsActivated || _task is not null)
        {
            throw new InvalidOperationException("Task already started.");
        }

        _task = System.Threading.Tasks.Task.Run(
            () => Execute(args, token),
            token
        );
        _isActivated = true;
    }

    private TResult Execute(TFirstArgs args, CancellationToken token)
    {
        InitializeEvents(_firstTask, 0);
        _firstTask.Activate(args, token);
        var firstResult = _firstTask.Task.Result;

        InitializeEventsForFinalTaskWithDifferentResult(
            _secondTask,
            secondResult => _resultSelector(args, firstResult, secondResult),
            _firstTask.StepCount
        );
        var secondArgs = _secondArgsSelector(args, firstResult);
        _secondTask.Activate(secondArgs, token);
        var secondResult = _secondTask.Task.Result;

        return _resultSelector(args, firstResult, secondResult);
    }

    private void ValidateInternalTasksNotActivated()
    {
        if (_firstTask.IsActivated)
        {
            throw new InvalidOperationException(
                "Could not wrap activated task. (First task is activated)"
            );
        }

        if (_secondTask.IsActivated)
        {
            throw new InvalidOperationException(
                "Could not wrap activated task. (Second task is activated)"
            );
        }
    }
}