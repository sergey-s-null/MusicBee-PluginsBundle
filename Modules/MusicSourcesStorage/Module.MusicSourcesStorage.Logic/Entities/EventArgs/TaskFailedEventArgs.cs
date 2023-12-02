namespace Module.MusicSourcesStorage.Logic.Entities.EventArgs;

public class TaskFailedEventArgs : System.EventArgs
{
    public Exception Exception { get; }

    public TaskFailedEventArgs(Exception exception)
    {
        Exception = exception;
    }
}