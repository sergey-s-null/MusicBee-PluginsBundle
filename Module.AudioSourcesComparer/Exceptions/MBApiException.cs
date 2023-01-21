using System;

namespace Module.AudioSourcesComparer.Exceptions
{
    public sealed class MBApiException : Exception
    {
        public MBApiException(string message) : base(message)
        {
        }
    }
}