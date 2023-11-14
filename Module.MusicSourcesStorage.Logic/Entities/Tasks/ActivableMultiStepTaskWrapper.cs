using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

namespace Module.MusicSourcesStorage.Logic.Entities.Tasks;

public sealed class ActivableMultiStepTaskWrapper<TArgs, TResult> :
    MultiStepTaskWrapperBase<TResult>,
    IActivableMultiStepTask<TArgs, TResult>
{
    public override int StepCount => 1;

    public override bool IsActivated => _internalTask.IsActivated;

    public override Task<TResult> Task => _internalTask.Task;

    private readonly IActivableTask<TArgs, TResult> _internalTask;

    public ActivableMultiStepTaskWrapper(IActivableTask<TArgs, TResult> internalTask)
    {
        _internalTask = internalTask;

        InitializeEventsForFinalTask(internalTask, 0);
    }

    public void Activate(TArgs args, CancellationToken token)
    {
        _internalTask.Activate(args, token);
    }
}