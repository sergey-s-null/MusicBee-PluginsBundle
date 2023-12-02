namespace Module.Mvvm.Extension.Exceptions;

public sealed class CallbackRegistrationException : Exception
{
    public CallbackRegistrationException(string message) : base(message)
    {
    }
}