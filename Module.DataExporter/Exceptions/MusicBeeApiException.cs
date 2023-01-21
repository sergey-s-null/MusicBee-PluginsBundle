using System;

namespace Module.DataExporter.Exceptions
{
    public sealed class MusicBeeApiException : Exception
    {
        public MusicBeeApiException()
        {
            
        }

        public MusicBeeApiException(string message) : base(message)
        {
            
        }
    }
}