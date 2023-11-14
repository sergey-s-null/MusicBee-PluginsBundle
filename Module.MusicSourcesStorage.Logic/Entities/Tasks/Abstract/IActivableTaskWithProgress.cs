namespace Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

public interface IActivableTaskWithProgress<in TArgs, TResult> : ITaskWithProgress<TResult>
{
    /// <exception cref="InvalidOperationException">Task already activated.</exception>
    void Activate(TArgs args, CancellationToken token = default);
}