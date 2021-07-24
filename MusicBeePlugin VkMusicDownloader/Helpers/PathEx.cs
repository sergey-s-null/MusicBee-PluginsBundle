using System.IO;

namespace VkMusicDownloader.Ex
{
    static class PathEx
    {
        public static string RemoveInvalidDirChars(string dirPath)
        {
            return string.Concat(dirPath.Split(Path.GetInvalidPathChars()));
        }

        public static string RemoveInvalidFileNameChars(string fileName)
        {
            return string.Concat(fileName.Split(Path.GetInvalidFileNameChars()));
        }
    }
}
