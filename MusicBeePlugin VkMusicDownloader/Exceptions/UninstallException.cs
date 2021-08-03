using System;

namespace VkMusicDownloader.Exceptions
{
    public class UninstallException : Exception
    {
        public UninstallException(string message = "", Exception innerException = null) 
            : base(message, innerException)
        {
            
        }
        

    }
}