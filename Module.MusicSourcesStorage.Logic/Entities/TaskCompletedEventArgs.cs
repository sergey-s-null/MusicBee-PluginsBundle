namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class TaskCompletedEventArgs : EventArgs
{
    public bool IsSucceeded { get; }
    public Exception? Exception { get; }

    public TaskCompletedEventArgs(bool isSucceeded, Exception? exception = null)
    {
        IsSucceeded = isSucceeded;
        Exception = exception;
    }
}