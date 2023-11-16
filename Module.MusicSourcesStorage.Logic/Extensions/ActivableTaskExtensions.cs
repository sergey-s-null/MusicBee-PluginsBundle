using Module.MusicSourcesStorage.Logic.Entities.Tasks;
using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;
using Void = Module.MusicSourcesStorage.Logic.Entities.Void;

namespace Module.MusicSourcesStorage.Logic.Extensions;

public static class ActivableTaskExtensions
{
    public static IActivableTaskWithoutCancellation<TArgs, TResult>
        WithToken<TArgs, TResult>(
            this IActivableTask<TArgs, TResult> task,
            CancellationToken token)
    {
        return new ActivableTaskWithTokenWrapper<TArgs, TResult>(task, token);
    }

    public static IActivableTask<Void, TResult>
        WithArgs<TArgs, TResult>(
            this IActivableTask<TArgs, TResult> task,
            TArgs args)
    {
        return new ActivableTaskWithArgsWrapper<TArgs, TResult>(task, args);
    }

    public static IActivableTask<TArgs, TResult> Activated<TArgs, TResult>(
        this IActivableTask<TArgs, TResult> task,
        TArgs args,
        CancellationToken token = default)
    {
        if (task.IsActivated)
        {
            return task;
        }

        task.Activate(args, token);
        return task;
    }

    public static IActivableMultiStepTask<TFirstArgs, TResult>
        Chain<TFirstArgs, TFirstResult, TSecondArgs, TResult>(
            this IActivableTask<TFirstArgs, TFirstResult> firstTask,
            Func<TFirstArgs, TFirstResult, TSecondArgs> secondArgsSelector,
            IActivableTask<TSecondArgs, TResult> secondTask
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

    public static IActivableTask<TChangedArgs, TResult>
        ChangeArgs<TArgs, TResult, TChangedArgs>(
            this IActivableTask<TArgs, TResult> task,
            Func<TChangedArgs, TArgs> oldArgsSelector
        )
    {
        return new ChangedArgsTaskWrapper<TArgs, TResult, TChangedArgs>(
            task,
            oldArgsSelector
        );
    }

    public static IActivableTask<TArgs, TChangedResult>
        ModifyResult<TArgs, TResult, TChangedResult>(
            this IActivableTask<TArgs, TResult> task,
            Func<TArgs, TResult, TChangedResult> resultSelector
        )
    {
        return new ActivableTaskWithChangedResultWrapper<TArgs, TResult, TChangedResult>(
            task,
            resultSelector
        );
    }

    public static IActivableMultiStepTask<TArgs, TResult>
        AsMultiStep<TArgs, TResult>(
            this IActivableTask<TArgs, TResult> task
        )
    {
        return new ActivableMultiStepTaskWrapper<TArgs, TResult>(task);
    }
}