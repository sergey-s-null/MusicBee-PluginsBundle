namespace Module.MusicSourcesStorage.Logic.Exceptions;

public sealed class LeafHasNodeNameException : Exception
{
    public LeafHasNodeNameException(string message) : base(message)
    {
    }

    public LeafHasNodeNameException(string message, Exception innerException) : base(message, innerException)
    {
    }
}