using System.IO;

namespace Module.VkAudioDownloader.Helpers;

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