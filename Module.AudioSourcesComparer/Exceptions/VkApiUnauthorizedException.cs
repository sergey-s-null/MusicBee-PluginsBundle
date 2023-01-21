using System;

namespace Module.AudioSourcesComparer.Exceptions
{
    public sealed class VkApiUnauthorizedException : Exception
    {
        public VkApiUnauthorizedException(string message) : base(message)
        {
        }
    }
}