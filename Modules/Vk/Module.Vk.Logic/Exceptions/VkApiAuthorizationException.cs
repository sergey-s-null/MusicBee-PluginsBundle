﻿namespace Module.Vk.Logic.Exceptions;

public sealed class VkApiAuthorizationException : Exception
{
    public VkApiAuthorizationException(string message) : base(message)
    {
    }

    public VkApiAuthorizationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}