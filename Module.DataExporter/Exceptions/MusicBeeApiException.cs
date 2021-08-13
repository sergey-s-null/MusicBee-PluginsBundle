using System;

namespace Module.DataExporter.Exceptions
{
    public class MusicBeeApiException : Exception
    {
        public MusicBeeApiException()
        {
            
        }

        public MusicBeeApiException(string message) : base(message)
        {
            
        }
    }
}