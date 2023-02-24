namespace Module.AudioSourcesComparer.Exceptions;

public sealed class VkApiInvalidValueException : Exception
{
    public VkApiInvalidValueException(string message) : base(message)
    {
    }
}