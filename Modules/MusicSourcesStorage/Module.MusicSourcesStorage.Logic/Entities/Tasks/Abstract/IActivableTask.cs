namespace Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

public interface IActivableTask<in TArgs, TResult> : ITask<TResult>
{
    /// <exception cref="InvalidOperationException">Task already activated.</exception>
    void Activate(TArgs args, CancellationToken token = default);
}