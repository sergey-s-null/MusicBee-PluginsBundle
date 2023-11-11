using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Extensions;

public static class ActivableTaskWithProgressExtensions
{
    public static IActivableWithoutCancellationTaskWithProgress<TResult> WithToken<TResult>(
        this IActivableTaskWithProgress<TResult> task,
        CancellationToken token)
    {
        return new ActivableTaskWithToken<TResult>(task, token);
    }

    public static IActivableTaskWithProgress<TResult> WithArgs<TArgs, TResult>(
        this IActivableTaskWithProgress<TArgs, TResult> task,
        TArgs args)
    {
        return new ActivableTaskWithArgs<TArgs, TResult>(task, args);
    }

    public static IActivableTaskWithProgress<TResult> Activated<TResult>(
        this IActivableTaskWithProgress<TResult> task,
        CancellationToken token = default)
    {
        if (task.IsActivated)
        {
            return task;
        }

        task.Activate(token);
        return task;
    }

    public static IActivableMultiStepTaskWithProgress<TFirstArgs, TResult>
        Chain<TFirstArgs, TFirstResult, TSecondArgs, TResult>(
            this IActivableTaskWithProgress<TFirstArgs, TFirstResult> firstTask,
            Func<TFirstArgs, TFirstResult, TSecondArgs> secondArgsSelector,
            IActivableTaskWithProgress<TSecondArgs, TResult> secondTask
        )
    {
        return firstTask
            .AsMultiStep()
            .Chain(
                secondArgsSelector,
                secondTask.AsMultiStep(),
                (_, _, result) => result
            );
    }

    public static IActivableTaskWithProgress<TChangedArgs, TResult>
        ChangeArgs<TArgs, TResult, TChangedArgs>(
            this IActivableTaskWithProgress<TArgs, TResult> task,
            Func<TChangedArgs, TArgs> oldArgsSelector
        )
    {
        return new ActivableTaskWithChangedArgsWrapper<TArgs, TResult, TChangedArgs>(
            task,
            oldArgsSelector
        );
    }

    public static IActivableTaskWithProgress<TArgs, TChangedResult>
        ModifyResult<TArgs, TResult, TChangedResult>(
            this IActivableTaskWithProgress<TArgs, TResult> task,
            Func<TArgs, TResult, TChangedResult> resultSelector
        )
    {
        return new ActivableTaskWithChangedResultWrapper<TArgs, TResult, TChangedResult>(
            task,
            resultSelector
        );
    }

    public static IActivableMultiStepTaskWithProgress<TArgs, TResult>
        AsMultiStep<TArgs, TResult>(
            this IActivableTaskWithProgress<TArgs, TResult> task
        )
    {
        return new ActivableMultiStepTaskWrapper<TArgs, TResult>(task);
    }
}