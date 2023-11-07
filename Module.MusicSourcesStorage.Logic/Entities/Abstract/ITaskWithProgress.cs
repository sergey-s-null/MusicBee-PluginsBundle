namespace Module.MusicSourcesStorage.Logic.Entities.Abstract;

public interface ITaskWithProgress<T>
{
    event EventHandler<ProgressChangedEventArgs> ProgressChanged;
    event EventHandler<TaskFailedEventArgs> Failed;
    event EventHandler Cancelled;
    event EventHandler<TaskResultEventArgs<T>> SuccessfullyCompleted;

    Task<T> Task { get; }

    void Activate();
}