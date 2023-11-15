﻿using Module.MusicSourcesStorage.Logic.Entities.Tasks;
using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;
using Void = Module.MusicSourcesStorage.Logic.Entities.Void;

namespace Module.MusicSourcesStorage.Logic.Extensions;

public static class ActivableMultiStepTaskExtensions
{
    public static IActivableMultiStepTask<Void, TResult>
        Activated<TResult>(
            this IActivableMultiStepTask<Void, TResult> task,
            CancellationToken token = default
        )
    {
        if (!task.IsActivated)
        {
            task.Activate(Void.Instance, token);
        }

        return task;
    }

    public static IActivableMultiStepTask<Void, TResult>
        WithArgs<TArgs, TResult>(
            this IActivableMultiStepTask<TArgs, TResult> task,
            TArgs args
        )
    {
        return new ActivableMultiStepTaskWithArgsWrapper<TArgs, TResult>(task, args);
    }

    public static IActivableMultiStepTask<TFirstArgs, TResult>
        Chain<TFirstArgs, TFirstResult, TResult>(
            this IActivableMultiStepTask<TFirstArgs, TFirstResult> firstTask,
            IActivableTask<TFirstResult, TResult> secondTask
        )
    {
        return firstTask.Chain(
            (_, result) => result,
            secondTask.AsMultiStep(),
            (_, _, result) => result
        );
    }

    public static IActivableMultiStepTask<TFirstArgs, TResult>
        Chain<TFirstArgs, TFirstResult, TSecondArgs, TSecondResult, TResult>(
            this IActivableMultiStepTask<TFirstArgs, TFirstResult> firstTask,
            Func<TFirstArgs, TFirstResult, TSecondArgs> secondArgsSelector,
            IActivableMultiStepTask<TSecondArgs, TSecondResult> secondTask,
            Func<TFirstArgs, TFirstResult, TSecondResult, TResult> resultSelector
        )
    {
        return new ChainedActivableMultiStepTasksWrapper<TFirstArgs, TFirstResult, TSecondArgs, TSecondResult, TResult>(
            firstTask,
            secondArgsSelector,
            secondTask,
            resultSelector
        );
    }
}