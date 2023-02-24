namespace Module.Core.Helpers;

public static class AsyncEnumerableHelper
{
    public static async Task<IReadOnlyCollection<T>> ToReadOnlyCollectionAsync<T>(this IAsyncEnumerable<T> items)
    {
        return await items.ToListAsync();
    }

    public static IAsyncEnumerable<T> WhereNotNull<T>(this IAsyncEnumerable<T?> items)
    {
        return items
            .Where(x => x is not null)
            .Select(x => x!);
    }
}