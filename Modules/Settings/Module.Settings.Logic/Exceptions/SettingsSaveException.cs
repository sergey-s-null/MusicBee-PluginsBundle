﻿namespace Module.Settings.Logic.Exceptions;

public sealed class SettingsSaveException : Exception
{
    public SettingsSaveException(string message) : base(message)
    {
    }

    public SettingsSaveException(string message, Exception innerException) : base(message, innerException)
    {
    }
}