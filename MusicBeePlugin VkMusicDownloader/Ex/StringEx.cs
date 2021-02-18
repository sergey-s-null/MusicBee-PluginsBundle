using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBeePlugin
{
    static class StringEx
    {
        /// <summary>
        /// Проверяем находится ли checkValue в baseValue в указанной позиции
        /// </summary>
        public static bool ContainsAt(this string str, string containValue, int pos)
        {
            if (str.Length - pos < containValue.Length)
                return false;

            return str.Substring(pos, containValue.Length) == containValue;
        }

        public static string Reverse(this string str)
        {
            char[] chars = str.ToArray();
            Array.Reverse(chars);
            return new string(chars);
        }
    }
}
