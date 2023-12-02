using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

namespace Module.MusicSourcesStorage.Logic.Entities.Tasks;

public sealed class ChangedArgsMultiStepTaskWrapper<TArgs, TResult, TChangedArgs> :
    MultiStepTaskWrapperBase<TResult>,
    IActivableMultiStepTask<TChangedArgs, TResult>
{
    public override int StepCount => _internalTask.StepCount;

    public override bool IsActivated => _internalTask.IsActivated;

    public override Task<TResult> Task => _internalTask.Task;

    private readonly IActivableMultiStepTask<TArgs, TResult> _internalTask;
    private readonly Func<TChangedArgs, TArgs> _oldArgsSelector;

    public ChangedArgsMultiStepTaskWrapper(
        IActivableMultiStepTask<TArgs, TResult> internalTask,
        Func<TChangedArgs, TArgs> oldArgsSelector)
    {
        _internalTask = internalTask;
        _oldArgsSelector = oldArgsSelector;

        ValidateInternalTaskNotActivated();

        InitializeEventsForFinalTask(_internalTask, 0);
    }

    public void Activate(TChangedArgs args, CancellationToken token)
    {
        _internalTask.Activate(_oldArgsSelector(args), token);
    }

    private void ValidateInternalTaskNotActivated()
    {
        if (_internalTask.IsActivated)
        {
            throw new InvalidOperationException("Could not wrap activated task.");
        }
    }
}