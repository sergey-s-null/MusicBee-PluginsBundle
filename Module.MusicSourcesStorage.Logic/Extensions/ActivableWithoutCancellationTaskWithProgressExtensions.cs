using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

namespace Module.MusicSourcesStorage.Logic.Extensions;

public static class ActivableWithoutCancellationTaskWithProgressExtensions
{
    public static IActivableWithoutCancellationTaskWithProgress<TResult> Activated<TResult>(
        this IActivableWithoutCancellationTaskWithProgress<TResult> task)
    {
        if (task.IsActivated)
        {
            return task;
        }

        task.Activate();
        return task;
    }

    public static IActivableWithoutCancellationTaskWithProgress<TResult> With<TResult>(
        this IActivableWithoutCancellationTaskWithProgress<TResult> task,
        Action<IActivableWithoutCancellationTaskWithProgress<TResult>> modifier)
    {
        modifier(task);
        return task;
    }
}