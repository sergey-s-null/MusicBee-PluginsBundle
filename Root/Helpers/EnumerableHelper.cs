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

        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> items)
        {
            return items
                .Where(x => x is not null)
                .Select(x => x!);
        }
    }
}