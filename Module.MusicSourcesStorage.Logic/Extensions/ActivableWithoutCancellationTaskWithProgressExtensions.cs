using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;
using Void = Module.MusicSourcesStorage.Logic.Entities.Void;

namespace Module.MusicSourcesStorage.Logic.Extensions;

public static class ActivableWithoutCancellationTaskWithProgressExtensions
{
    public static IActivableWithoutCancellationTaskWithProgress<Void, TResult>
        Activated<TResult>(
            this IActivableWithoutCancellationTaskWithProgress<Void, TResult> task
        )
    {
        if (!task.IsActivated)
        {
            task.Activate(Void.Instance);
        }

        return task;
    }
}