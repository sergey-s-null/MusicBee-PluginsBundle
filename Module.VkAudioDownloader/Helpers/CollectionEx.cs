namespace Module.VkAudioDownloader.Helpers;

public static class CollectionEx
{
    public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> enumerable)
    {
        foreach (var item in enumerable)
        {
            collection.Add(item);
        }
    }
}