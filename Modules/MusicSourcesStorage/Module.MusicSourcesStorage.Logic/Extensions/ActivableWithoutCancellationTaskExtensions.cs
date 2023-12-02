using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;
using Void = Module.MusicSourcesStorage.Logic.Entities.Void;

namespace Module.MusicSourcesStorage.Logic.Extensions;

public static class ActivableWithoutCancellationTaskExtensions
{
    public static IActivableTaskWithoutCancellation<Void, TResult>
        Activated<TResult>(
            this IActivableTaskWithoutCancellation<Void, TResult> task
        )
    {
        if (!task.IsActivated)
        {
            task.Activate(Void.Instance);
        }

        return task;
    }
}