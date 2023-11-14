namespace Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

public interface IActivableTaskWithoutCancellation<in TArgs, TResult> :
    ITask<TResult>
{
    /// <exception cref="InvalidOperationException">Task already activated.</exception>
    void Activate(TArgs args);
}