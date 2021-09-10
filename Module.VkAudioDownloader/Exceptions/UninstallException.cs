using System;

namespace Module.VkAudioDownloader.Exceptions
{
    public class UninstallException : Exception
    {
        public UninstallException(string message = "", Exception innerException = null) 
            : base(message, innerException)
        {
            
        }
        

    }
}