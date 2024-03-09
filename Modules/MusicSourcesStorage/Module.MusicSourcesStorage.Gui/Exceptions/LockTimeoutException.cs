namespace Module.MusicSourcesStorage.Gui.Exceptions;

public sealed class LockTimeoutException : Exception
{
    public LockTimeoutException(string message) : base(message)
    {
    }

    public LockTimeoutException(string message, Exception innerException) : base(message, innerException)
    {
    }
}