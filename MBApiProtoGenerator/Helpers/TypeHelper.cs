using System;
using System.Collections;
using System.Linq;

namespace MBApiProtoGenerator.Helpers
{
    public static class TypeHelper
    {
        public static bool IsEnumerable(this Type type, out Type? elementType)
        {
            if (type.IsGenericType
                && typeof(IEnumerable).IsAssignableFrom(type.GetGenericTypeDefinition()))
            {
                elementType = type.GenericTypeArguments.First();
                return true;
            }

            if (type.IsArray && type.HasElementType)
            {
                elementType = type.GetElementType();
                return true;
            }

            elementType = null;
            return false;
        }
        
        public static bool IsEnumerable(this Type type)
        {
            if (type.IsGenericType
                && typeof(IEnumerable).IsAssignableFrom(type.GetGenericTypeDefinition()))
            {
                return true;
            }

            if (type.IsArray && type.HasElementType)
            {
                return true;
            }

            return false;
        }

        public static Type RemoveRefWrapper(this Type type)
        {
            if (type.IsByRef && type.HasElementType)
            {
                return type.GetElementType()!;
            }

            return type;
        }
    }
}