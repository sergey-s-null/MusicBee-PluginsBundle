namespace Module.VkAudioDownloader.Extensions;

internal static class EnumerableExtensions
{
    public static IEnumerable<(TFirst, TSecond)> Zip<TFirst, TSecond>(
        this IEnumerable<TFirst> first,
        IEnumerable<TSecond> seconds)
    {
        return first.Zip(seconds, (x, y) => (x, y));
    }
}