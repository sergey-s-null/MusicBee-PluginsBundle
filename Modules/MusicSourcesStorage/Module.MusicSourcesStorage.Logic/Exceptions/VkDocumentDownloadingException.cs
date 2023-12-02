namespace Module.MusicSourcesStorage.Logic.Exceptions;

public sealed class VkDocumentDownloadingException : Exception
{
    public VkDocumentDownloadingException(string message) : base(message)
    {
    }

    public VkDocumentDownloadingException(string message, Exception innerException) : base(message, innerException)
    {
    }
}