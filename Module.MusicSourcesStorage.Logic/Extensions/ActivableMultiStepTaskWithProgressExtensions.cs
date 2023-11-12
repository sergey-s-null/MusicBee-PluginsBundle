using Module.MusicSourcesStorage.Logic.Entities.Tasks;
using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

namespace Module.MusicSourcesStorage.Logic.Extensions;

public static class ActivableMultiStepTaskWithProgressExtensions
{
    public static IActivableMultiStepTaskWithProgress<TResult> Activated<TResult>(
        this IActivableMultiStepTaskWithProgress<TResult> task,
        CancellationToken token = default)
    {
        if (!task.IsActivated)
        {
            task.Activate(token);
        }

        return task;
    }

    public static IActivableMultiStepTaskWithProgress<TResult>
        WithArgs<TArgs, TResult>(
            this IActivableMultiStepTaskWithProgress<TArgs, TResult> task,
            TArgs args
        )
    {
        return new ActivableMultiStepTaskWithArgsWrapper<TArgs, TResult>(task, args);
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