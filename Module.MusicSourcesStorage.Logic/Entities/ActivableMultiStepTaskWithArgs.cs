using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class ActivableMultiStepTaskWithArgs<TArgs, TResult> :
    MultiStepTaskWrapperBase<TResult>,
    IActivableMultiStepTaskWithProgress<TResult>
{
    public override int StepCount => _internalTask.StepCount;

    public override bool IsActivated => _internalTask.IsActivated;

    public override Task<TResult> Task => _internalTask.Task;

    private readonly IActivableMultiStepTaskWithProgress<TArgs, TResult> _internalTask;
    private readonly TArgs _args;

    public ActivableMultiStepTaskWithArgs(
        IActivableMultiStepTaskWithProgress<TArgs, TResult> internalTask,
        TArgs args)
    {
        _internalTask = internalTask;
        _args = args;

        InitializeEventsForFinalTask(_internalTask, 0);
    }

    public void Activate(CancellationToken token)
    {
        _internalTask.Activate(_args, token);
    }
}