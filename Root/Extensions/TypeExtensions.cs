using System;
using System.Collections.Generic;

namespace Root.Extensions
{
    public static class TypeExtensions
    {
        private static readonly IReadOnlyDictionary<Type, string> BaseTypesMappings = new Dictionary<Type, string>
        {
            [typeof(string)] = "string",
            [typeof(bool)] = "bool",
            [typeof(void)] = "void",
            [typeof(int)] = "int",
            [typeof(byte)] = "byte",
            [typeof(float)] = "float",
            [typeof(double)] = "double",
            [typeof(object)] = "object"
        };

        /// <summary>
        /// Заменяет базовые типы:
        ///     String -> string
        ///     Int32 -> int
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetFixedName(this Type type)
        {
            return BaseTypesMappings.TryGetValue(type, out var stringType)
                ? stringType
                : type.Name;
        }
    }
}