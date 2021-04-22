using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkMusicDownloader.Ex
{
    public static class CollectionEx
    {
        public static void AddRange<T>(this Collection<T> collection, IEnumerable<T> enumerable)
        {
            foreach (T item in enumerable)
            {
                collection.Add(item);
            }
        }
    }
}
