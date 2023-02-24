namespace Module.Core.Helpers;

public static class EnumerableHelper
{
    public static IReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> items)
    {
        return items.ToList();
    }

    public static IReadOnlyList<T> ToReadOnlyList<T>(this IEnumerable<T> items)
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