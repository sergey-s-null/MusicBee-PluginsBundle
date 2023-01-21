using System;

namespace Module.Settings.Exceptions;

public sealed class SettingsSaveException : Exception
{
    public SettingsSaveException(string message) : base(message)
    {
    }

    public SettingsSaveException(string message, Exception innerException) : base(message, innerException)
    {
    }
}