namespace Module.AudioSourcesComparer.Exceptions;

public sealed class MBLibraryInvalidStateException : Exception
{
    public MBLibraryInvalidStateException(string message) : base(message)
    {
    }
}