namespace Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;

public interface ISharedTask<TResult> : IActivableTaskWithoutCancellation<Void, TResult>
{
    /// <summary>
    /// Signals that new external task owned current shared task.
    /// </summary>
    void Acquire();

    /// <summary>
    /// Signals that one external task stopped being owner of current shared task.
    /// </summary>
    void Release();
}