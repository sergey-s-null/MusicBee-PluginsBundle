﻿using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VkMusicDownloader.Helpers
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