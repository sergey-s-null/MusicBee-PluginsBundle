using System;

namespace CoreModule.Vk.Exceptions
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