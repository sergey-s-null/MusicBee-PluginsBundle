namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class TaskResultEventArgs<T> : EventArgs
{
    public T Result { get; }

    public TaskResultEventArgs(T result)
    {
        Result = result;
    }
}