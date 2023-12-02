using Module.MusicSourcesStorage.Logic.Entities.Tasks;
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

    public static IActivableMultiStepTask<TArgs, TResult>
        Activated<TArgs, TResult>(
            this IActivableMultiStepTask<TArgs, TResult> task,
            TArgs args,
            CancellationToken token = default
        )
    {
        if (!task.IsActivated)
        {
            task.Activate(args, token);
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

    public static IActivableMultiStepTask<TChangedArgs, TResult>
        ChangeArgs<TArgs, TResult, TChangedArgs>(
            this IActivableMultiStepTask<TArgs, TResult> task,
            Func<TChangedArgs, TArgs> oldArgsSelector
        )
    {
        return new ChangedArgsMultiStepTaskWrapper<TArgs, TResult, TChangedArgs>(
            task,
            oldArgsSelector
        );
    }

    public static IActivableMultiStepTask<TArgs, TResult>
        SkipUnderCondition<TArgs, TResult>(
            this IActivableMultiStepTask<TArgs, TResult> task,
            Func<TArgs, bool> skipCondition,
            Func<TArgs, TResult> skipResultSelector
        )
    {
        return new SkipUnderConditionMultiStepTaskWrapper<TArgs, TResult>(
            task,
            skipCondition,
            skipResultSelector
        );
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
        Chain<TFirstArgs, TFirstResult, TSecondResult, TResult>(
            this IActivableMultiStepTask<TFirstArgs, TFirstResult> firstTask,
            IActivableTask<TFirstResult, TSecondResult> secondTask,
            Func<TFirstArgs, TFirstResult, TSecondResult, TResult> resultSelector
        )
    {
        return firstTask.Chain(
            (_, result) => result,
            secondTask,
            resultSelector
        );
    }

    public static IActivableMultiStepTask<TFirstArgs, TResult>
        Chain<TFirstArgs, TFirstResult, TSecondArgs, TSecondResult, TResult>(
            this IActivableMultiStepTask<TFirstArgs, TFirstResult> firstTask,
            Func<TFirstArgs, TFirstResult, TSecondArgs> secondArgsSelector,
            IActivableTask<TSecondArgs, TSecondResult> secondTask,
            Func<TFirstArgs, TFirstResult, TSecondResult, TResult> resultSelector
        )
    {
        return firstTask.Chain(
            secondArgsSelector,
            secondTask.AsMultiStep(),
            resultSelector
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