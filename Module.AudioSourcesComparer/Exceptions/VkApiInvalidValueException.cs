using System;

namespace Module.AudioSourcesComparer.Exceptions
{
    public class VkApiInvalidValueException : Exception
    {
        public VkApiInvalidValueException(string message) : base(message)
        {
        }
    }
}