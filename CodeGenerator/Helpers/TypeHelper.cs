using System;
using System.Collections;

namespace CodeGenerator.Helpers
{
    public static class TypeHelper
    {
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
    }
}