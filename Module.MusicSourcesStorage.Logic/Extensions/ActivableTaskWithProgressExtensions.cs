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
}