namespace Module.MusicSourcesStorage.Logic.Entities;

public class TaskFailedEventArgs : EventArgs
{
    public Exception Exception { get; }

    public TaskFailedEventArgs(Exception exception)
    {
        Exception = exception;
    }
}