using System;
using System.IO;

namespace Root.Helpers
{
    public static class PathHelper
    {
        public static string GetRelativeToDirectoryPath(string relativeTo, string path)
        {
            // ToDirectoryPath вызывается для того, чтобы 
            //      "D:\_BIG_FILES_\Music Library"
            //      "D:\_BIG_FILES_\Music Library\Linkin Park"
            // вернуло "Linkin Park", а не "Music Library\Linkin Park"
            return GetRelativePath(ToDirectoryPath(relativeTo), path);
        }
        
        public static string GetRelativePath(string relativeTo, string path)
        {
            var relativeToUri = new Uri(relativeTo);
            var pathUri = new Uri(path);
            var relativeUri = relativeToUri.MakeRelativeUri(pathUri);

            return Uri
                .UnescapeDataString(relativeUri.ToString())
                .Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
        }

        /// <summary>
        /// Добавляет в конец строки символ "/" разделения пути
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ToDirectoryPath(string path)
        {
            if (!path.EndsWith(Path.DirectorySeparatorChar.ToString())
                && !path.EndsWith(Path.AltDirectorySeparatorChar.ToString()))
            {
                return path + Path.DirectorySeparatorChar;
            }

            return path;
        }
    }
}