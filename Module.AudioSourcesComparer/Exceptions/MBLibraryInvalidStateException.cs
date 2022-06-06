using System;

namespace Module.AudioSourcesComparer.Exceptions
{
    public class MBLibraryInvalidStateException : Exception
    {
        public MBLibraryInvalidStateException(string message) : base(message)
        {
        }
    }
}