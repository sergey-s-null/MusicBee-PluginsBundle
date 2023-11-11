namespace Module.MusicSourcesStorage.Logic.Entities.Abstract;

public interface IActivableWithoutCancellationTaskWithProgress<TResult> :
    ITaskWithProgress<TResult>
{
    /// <exception cref="InvalidOperationException">Task already activated.</exception>
    void Activate();
}