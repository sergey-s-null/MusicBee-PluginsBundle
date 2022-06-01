using System.Collections.Generic;
using System.Linq;

namespace Root.Helpers
{
    public static class SetHelper
    {
        public static ISet<T> ExceptCopy<T>(this ISet<T> set, IEnumerable<T> other)
        {
            var copy = set.ToHashSet();
            copy.ExceptWith(other);
            return copy;
        }
    }
}