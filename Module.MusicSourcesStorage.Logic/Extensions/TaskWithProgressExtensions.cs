using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Extensions;

public static class TaskWithProgressExtensions
{
    public static IMultiStepTaskWithProgress<TResult> Chain<TFirstResult, TResult>(
        this ITaskWithProgress<TFirstResult> firstTask,
        Func<TFirstResult, ITaskWithProgress<TResult>> secondTaskFactory)
    {
        return new ChainedTasksPair<TFirstResult, TResult>(firstTask, secondTaskFactory);
    }
}