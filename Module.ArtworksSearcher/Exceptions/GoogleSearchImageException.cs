using System;

namespace Module.ArtworksSearcher.Exceptions
{
    public class GoogleSearchImageException : Exception
    {
        public GoogleSearchImageException(string message) : base(message)
        {
        }

        public GoogleSearchImageException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}