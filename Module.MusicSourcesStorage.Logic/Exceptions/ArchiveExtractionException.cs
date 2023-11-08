namespace Module.MusicSourcesStorage.Logic.Exceptions;

public sealed class ArchiveExtractionException : Exception
{
    public ArchiveExtractionException(string message) : base(message)
    {
    }

    public ArchiveExtractionException(string message, Exception innerException) : base(message, innerException)
    {
    }
}