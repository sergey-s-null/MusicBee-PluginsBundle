using System.IO;

namespace VkMusicDownloader.Ex
{
    static class FileEx
    {
        public static bool TryDelete(string filePath)
        {
            try
            {
                File.Delete(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
