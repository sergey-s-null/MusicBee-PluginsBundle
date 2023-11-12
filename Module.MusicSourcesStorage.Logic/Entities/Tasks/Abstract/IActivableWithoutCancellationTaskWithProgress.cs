namespace Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

public interface IActivableWithoutCancellationTaskWithProgress<TResult> :
    ITaskWithProgress<TResult>
{
    /// <exception cref="InvalidOperationException">Task already activated.</exception>
    void Activate();
}