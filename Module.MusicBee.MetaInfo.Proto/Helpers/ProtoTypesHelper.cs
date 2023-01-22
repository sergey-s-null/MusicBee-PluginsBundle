using System;
using System.Collections.Generic;
using Module.MusicBee.MetaInfo.Proto.Extensions;

namespace Module.MusicBee.MetaInfo.Proto.Helpers;

public static class ProtoTypesHelper
{
    private static readonly IReadOnlyDictionary<Type, string> TypeBindings = new Dictionary<Type, string>()
    {
        [typeof(bool)] = "bool",
        [typeof(string)] = "string",
        [typeof(int)] = "int32",
        [typeof(float)] = "float",
        [typeof(double)] = "double",
    };

    public static string GetProtoType(Type parameterType, bool withEnumerable = true)
    {
        if (parameterType.IsByRef && parameterType.HasElementType)
        {
            return GetProtoType(parameterType.GetElementType()!);
        }

        if (parameterType.IsEnumerable(out var elementType))
        {
            if (elementType == typeof(byte))
            {
                return "bytes";
            }

            if (withEnumerable)
            {
                var elementProtoType = GetProtoType(elementType!, false);
                return $"repeated {elementProtoType}";
            }
        }

        if (parameterType.IsEnum
            && TypeBindings.TryGetValue(typeof(int), out var intProtoType))
        {
            return intProtoType;
        }

        if (TypeBindings.TryGetValue(parameterType, out var protoType))
        {
            return protoType;
        }

        throw new Exception($"Not found matching proto type for type {parameterType.FullName}.");
    }
}