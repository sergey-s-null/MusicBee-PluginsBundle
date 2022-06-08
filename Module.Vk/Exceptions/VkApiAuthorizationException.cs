using System;

namespace Module.Vk.Exceptions
{
    public class VkApiAuthorizationException : Exception
    {
        public VkApiAuthorizationException(string message) : base(message)
        {
        }

        public VkApiAuthorizationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}