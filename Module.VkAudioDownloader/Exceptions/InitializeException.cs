namespace Module.VkAudioDownloader.Exceptions;

public class InitializeException : Exception
{
    public InitializeException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}