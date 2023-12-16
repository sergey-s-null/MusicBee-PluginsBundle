namespace Module.MusicSourcesStorage.Logic.Exceptions;

public sealed class LeavesDuplicationException : Exception
{
    public LeavesDuplicationException(string message) : base(message)
    {
    }

    public LeavesDuplicationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}