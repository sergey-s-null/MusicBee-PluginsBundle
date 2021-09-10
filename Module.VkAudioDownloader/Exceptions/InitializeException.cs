using System;

namespace Module.VkAudioDownloader.Exceptions
{
    public class InitializeException : Exception
    {
        public InitializeException(string message = "", Exception innerException = null) 
            : base(message, innerException)
        {
            
        }
        

    }
}