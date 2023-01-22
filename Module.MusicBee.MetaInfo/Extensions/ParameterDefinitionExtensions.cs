using System;
using Module.MusicBee.MetaInfo.Entities;
using Root.Extensions;

namespace Module.MusicBee.MetaInfo.Extensions;

public static class ParameterDefinitionExtensions
{
    public static string GetCSharpTypeName(this ParameterDefinition parameter)
    {
        var csTypeName = GetCSharpTypeName(parameter.Type);
        if (parameter.IsNullable)
        {
            csTypeName += "?";
        }

        return csTypeName;
    }

    private static string GetCSharpTypeName(Type type)
    {
        if (type.IsArray && type.HasElementType)
        {
            return $"{GetCSharpTypeName(type.GetElementType()!)}[]";
        }

        return type.GetFixedName();
    }
}