using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ArtworksSearcher.Ex
{
    public static class WebClientEx
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
