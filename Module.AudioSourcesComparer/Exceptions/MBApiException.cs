using System;

namespace Module.AudioSourcesComparer.Exceptions
{
    public class MBApiException : Exception
    {
        public MBApiException(string message) : base(message)
        {
        }
    }
}