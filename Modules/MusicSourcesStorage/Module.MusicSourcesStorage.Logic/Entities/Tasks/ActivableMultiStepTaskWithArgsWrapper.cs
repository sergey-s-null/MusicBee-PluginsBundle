﻿using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

namespace Module.MusicSourcesStorage.Logic.Entities.Tasks;

public sealed class ActivableMultiStepTaskWithArgsWrapper<TArgs, TResult> :
    MultiStepTaskWrapperBase<TResult>,
    IActivableMultiStepTask<Void, TResult>
{
    public override int StepCount => _internalTask.StepCount;

    public override bool IsActivated => _internalTask.IsActivated;

    public override Task<TResult> Task => _internalTask.Task;

    private readonly IActivableMultiStepTask<TArgs, TResult> _internalTask;
    private readonly TArgs _args;

    public ActivableMultiStepTaskWithArgsWrapper(
        IActivableMultiStepTask<TArgs, TResult> internalTask,
        TArgs args)
    {
        _internalTask = internalTask;
        _args = args;

        InitializeEventsForFinalTask(_internalTask, 0);
    }

    public void Activate(Void _, CancellationToken token)
    {
        _internalTask.Activate(_args, token);
    }
}