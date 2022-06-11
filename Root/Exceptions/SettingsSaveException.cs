using System;

namespace Root.Exceptions
{
    public class SettingsSaveException : Exception
    {
        public SettingsSaveException(string message) : base(message)
        {
        }

        public SettingsSaveException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}