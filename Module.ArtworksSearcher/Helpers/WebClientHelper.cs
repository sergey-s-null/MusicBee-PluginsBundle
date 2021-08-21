using System.Net;

namespace Module.ArtworksSearcher.Helpers
{
    public static class WebClientHelper
    {
        public static bool TryDownloadData(this WebClient webClient, string address, out byte[] data)
        {
            try
            {
                data = webClient.DownloadData(address);
                return true;
            }
            catch (WebException)
            {
                data = null;
                return false;
            }
        }
    }
}
