namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class TaskFailedEventArgs : EventArgs
{
    public Exception Exception { get; }

    public TaskFailedEventArgs(Exception exception)
    {
        Exception = exception;
    }
}