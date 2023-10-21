namespace Module.MusicSourcesStorage.Logic.Exceptions;

public sealed class ArchiveIndexingException : Exception
{
    public ArchiveIndexingException(string message) : base(message)
    {
    }

    public ArchiveIndexingException(string message, Exception innerException) : base(message, innerException)
    {
    }
}