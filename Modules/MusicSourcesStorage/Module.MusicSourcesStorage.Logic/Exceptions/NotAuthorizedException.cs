namespace Module.MusicSourcesStorage.Logic.Exceptions;

public sealed class NotAuthorizedException : Exception
{
    public NotAuthorizedException(string message) : base(message)
    {
    }

    public NotAuthorizedException(string message, Exception innerException) : base(message, innerException)
    {
    }
}