namespace Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

public interface IActivableTaskWithProgress<TResult> : ITaskWithProgress<TResult>
{
    /// <exception cref="InvalidOperationException">Task already activated.</exception>
    void Activate(CancellationToken token = default);
}

public interface IActivableTaskWithProgress<in TArgs, TResult> : ITaskWithProgress<TResult>
{
    /// <exception cref="InvalidOperationException">Task already activated.</exception>
    void Activate(TArgs args, CancellationToken token = default);
}