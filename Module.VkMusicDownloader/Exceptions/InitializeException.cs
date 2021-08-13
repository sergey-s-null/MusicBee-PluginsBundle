using System;

namespace Module.VkMusicDownloader.Exceptions
{
    public class InitializeException : Exception
    {
        public InitializeException(string message = "", Exception innerException = null) 
            : base(message, innerException)
        {
            
        }
        

    }
}