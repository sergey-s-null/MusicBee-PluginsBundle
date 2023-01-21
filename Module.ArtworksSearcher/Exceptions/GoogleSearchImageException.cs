using System;

namespace Module.ArtworksSearcher.Exceptions
{
    public sealed class GoogleSearchImageException : Exception
    {
        public GoogleSearchImageException(string message) : base(message)
        {
        }

        public GoogleSearchImageException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}