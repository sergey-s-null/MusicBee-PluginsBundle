using System.Collections.Generic;
using System.Linq;

namespace Root.Helpers
{
    public static class EnumerableHelper
    {
        public static IReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> items)
        {
            return items.ToList();
        }
    }
}