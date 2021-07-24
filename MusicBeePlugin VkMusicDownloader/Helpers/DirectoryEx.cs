using System.IO;

namespace VkMusicDownloader.Ex
{
    static class DirectoryEx
    {
        public static bool TryCreateDirectory(string dirPath)
        {
            if (Directory.Exists(dirPath))
                return true;

            try
            {
                Directory.CreateDirectory(dirPath);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
