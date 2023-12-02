namespace Module.MusicSourcesStorage.Logic.Entities.EventArgs;

public sealed class TaskResultEventArgs<T> : System.EventArgs
{
    public T Result { get; }

    public TaskResultEventArgs(T result)
    {
        Result = result;
    }
}