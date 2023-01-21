using System;

namespace Module.MusicBee.Extension.LibraryApi.Exceptions;

public sealed class FilesRetrievingException : Exception
{
    public FilesRetrievingException(string message) : base(message)
    {
    }

    public FilesRetrievingException(string message, Exception innerException) : base(message, innerException)
    {
    }
}