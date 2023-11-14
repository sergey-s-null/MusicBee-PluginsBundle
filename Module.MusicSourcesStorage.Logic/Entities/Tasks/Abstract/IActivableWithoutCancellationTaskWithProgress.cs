namespace Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

// todo rename IActivableTaskWithoutCancellation
public interface IActivableWithoutCancellationTaskWithProgress<in TArgs, TResult> :
    ITaskWithProgress<TResult>
{
    /// <exception cref="InvalidOperationException">Task already activated.</exception>
    void Activate(TArgs args);
}