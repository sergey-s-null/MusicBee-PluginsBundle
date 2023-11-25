namespace Module.MusicSourcesStorage.Logic.Exceptions;

public sealed class FileLocatingException : Exception
{
    public FileLocatingException(string message) : base(message)
    {
    }

    public FileLocatingException(string message, Exception innerException) : base(message, innerException)
    {
    }
}