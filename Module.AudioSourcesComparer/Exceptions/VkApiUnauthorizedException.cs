using System;

namespace Module.AudioSourcesComparer.Exceptions
{
    public class VkApiUnauthorizedException : Exception
    {
        public VkApiUnauthorizedException(string message) : base(message)
        {
        }
    }
}