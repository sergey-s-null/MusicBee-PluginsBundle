using System.Net;
using System.Threading.Tasks;

namespace VkMusicDownloader.Ex
{
    public static class WebClientEx
    {
        public static async Task DownloadFileAsync(string address, string fileName)
        {
            using (WebClient webClient = new WebClient())
            {
                await webClient.DownloadFileTaskAsync(address, fileName);
            }
        }

        public static async Task<string> DownloadStringAsync(string address)
        {
            using (WebClient webClient = new WebClient())
            {
                return await webClient.DownloadStringTaskAsync(address);
            }
        }

        public static async Task<byte[]> DownloadDataAsync(string address)
        {
            using (WebClient webClient = new WebClient())
            {
                return await webClient.DownloadDataTaskAsync(address);
            }
        }
    }
}
