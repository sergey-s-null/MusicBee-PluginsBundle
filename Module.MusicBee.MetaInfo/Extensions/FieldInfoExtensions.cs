using System;
using System.Reflection;

namespace Module.MusicBee.MetaInfo.Extensions;

internal static class FieldInfoExtensions
{
    public static bool IsDelegate(this FieldInfo fieldInfo)
    {
        return typeof(Delegate).IsAssignableFrom(fieldInfo.FieldType);
    }
}