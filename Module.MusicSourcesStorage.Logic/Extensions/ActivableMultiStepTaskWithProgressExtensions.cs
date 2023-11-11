using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Extensions;

public static class ActivableMultiStepTaskWithProgressExtensions
{
    public static IActivableMultiStepTaskWithProgress<TFirstArgs, TResult>
        Chain<TFirstArgs, TFirstResult, TSecondArgs, TSecondResult, TResult>(
            this IActivableMultiStepTaskWithProgress<TFirstArgs, TFirstResult> firstTask,
            Func<TFirstArgs, TFirstResult, TSecondArgs> secondArgsSelector,
            IActivableMultiStepTaskWithProgress<TSecondArgs, TSecondResult> secondTask,
            Func<TFirstArgs, TFirstResult, TSecondResult, TResult> resultSelector
        )
    {
        return new ChainedActivableMultiStepTasks<TFirstArgs, TFirstResult, TSecondArgs, TSecondResult, TResult>(
            firstTask,
            secondArgsSelector,
            secondTask,
            resultSelector
        );
    }
}