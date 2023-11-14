using Module.MusicSourcesStorage.Logic.Entities.Tasks;
using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;
using Void = Module.MusicSourcesStorage.Logic.Entities.Void;

namespace Module.MusicSourcesStorage.Logic.Extensions;

public static class ActivableMultiStepTaskWithProgressExtensions
{
    public static IActivableMultiStepTaskWithProgress<Void, TResult>
        Activated<TResult>(
            this IActivableMultiStepTaskWithProgress<Void, TResult> task,
            CancellationToken token = default
        )
    {
        if (!task.IsActivated)
        {
            task.Activate(Void.Instance, token);
        }

        return task;
    }

    public static IActivableMultiStepTaskWithProgress<Void, TResult>
        WithArgs<TArgs, TResult>(
            this IActivableMultiStepTaskWithProgress<TArgs, TResult> task,
            TArgs args
        )
    {
        return new ActivableMultiStepTaskWithArgsWrapper<TArgs, TResult>(task, args);
    }

    public static IActivableMultiStepTaskWithProgress<TFirstArgs, TResult>
        Chain<TFirstArgs, TFirstResult, TResult>(
            this IActivableMultiStepTaskWithProgress<TFirstArgs, TFirstResult> firstTask,
            IActivableTaskWithProgress<TFirstResult, TResult> secondTask
        )
    {
        return firstTask.Chain(
            (_, result) => result,
            secondTask.AsMultiStep(),
            (_, _, result) => result
        );
    }

    public static IActivableMultiStepTaskWithProgress<TFirstArgs, TResult>
        Chain<TFirstArgs, TFirstResult, TSecondArgs, TSecondResult, TResult>(
            this IActivableMultiStepTaskWithProgress<TFirstArgs, TFirstResult> firstTask,
            Func<TFirstArgs, TFirstResult, TSecondArgs> secondArgsSelector,
            IActivableMultiStepTaskWithProgress<TSecondArgs, TSecondResult> secondTask,
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