using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

namespace Module.MusicSourcesStorage.Logic.Entities.Tasks;

public sealed class SkipUnderConditionMultiStepTaskWrapper<TArgs, TResult> :
    MultiStepTaskWrapperBase<TResult>,
    IActivableMultiStepTask<TArgs, TResult>
{
    public override int StepCount => _internalTask.StepCount;

    public override bool IsActivated => _isActivated;

    public override Task<TResult> Task => IsActivated && _task is not null
        ? _task
        : throw new InvalidOperationException("Task is not activated");

    private bool _isActivated;
    private Task<TResult>? _task;
    private readonly IActivableMultiStepTask<TArgs, TResult> _internalTask;
    private readonly Func<TArgs, bool> _skipCondition;
    private readonly Func<TArgs, TResult> _skipResultSelector;

    public SkipUnderConditionMultiStepTaskWrapper(
        IActivableMultiStepTask<TArgs, TResult> internalTask,
        Func<TArgs, bool> skipCondition,
        Func<TArgs, TResult> skipResultSelector)
    {
        _internalTask = internalTask;
        _skipCondition = skipCondition;
        _skipResultSelector = skipResultSelector;

        CheckInternalTaskIsNotActivated();
    }

    public void Activate(TArgs args, CancellationToken token)
    {
        if (IsActivated || _task is not null)
        {
            throw new InvalidOperationException("Task already activated");
        }

        if (_skipCondition(args))
        {
            var result = _skipResultSelector(args);
            _task = System.Threading.Tasks.Task.FromResult(result);
            _isActivated = true;
            DispatchEventsToSimulateFullCompletion(result);
        }
        else
        {
            InitializeEventsForFinalTask(_internalTask, 0);
            _internalTask.Activate(args, token);

            _task = _internalTask.Task;
            _isActivated = true;
        }
    }

    private void CheckInternalTaskIsNotActivated()
    {
        if (_internalTask.IsActivated)
        {
            throw new InvalidOperationException("Internal task already activated.");
        }
    }
}