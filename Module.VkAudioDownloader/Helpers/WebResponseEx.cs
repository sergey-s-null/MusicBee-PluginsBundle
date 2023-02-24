using System.IO;
using System.Net;

namespace Module.VkAudioDownloader.Helpers;

public static class WebResponseEx
{
    public static string ReadAllText(this WebResponse response)
    {
        using (var reader = new StreamReader(response.GetResponseStream()))
        {
            return reader.ReadToEnd();
        }
    }
}