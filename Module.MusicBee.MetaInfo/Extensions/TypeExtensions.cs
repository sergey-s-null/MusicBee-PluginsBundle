using System;

namespace Module.MusicBee.MetaInfo.Extensions;

public static class TypeExtensions
{
    public static Type RemoveRefWrapper(this Type type)
    {
        if (type.IsByRef && type.HasElementType)
        {
            return type.GetElementType()!;
        }

        return type;
    }
}