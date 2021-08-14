using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Media.Imaging;
using Module.ArtworksSearcher.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Root.Abstractions;

namespace Module.ArtworksSearcher.ImagesProviders
{
    public class GoogleImagesProvider : IImagesProvider
    {
        private const string GoogleApiUrl = "https://www.googleapis.com/customsearch/v1";
        private readonly string _cx;
        private readonly string _key;

        public GoogleImagesProvider(string cx, string key)
        {
            _cx = cx;
            _key = key;
        }

        public IEnumerable<BitmapImage> GetImagesIter(string query)
        {
            foreach (var data in GetBinaryIter(query))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = new MemoryStream(data);
                image.EndInit();
                yield return image;
            }
        }

        public IEnumerable<byte[]> GetBinaryIter(string query)
        {
            foreach (var imgUrl in GetImgUrlIter(query))
            {
                using var webClient = new WebClient();
                if (webClient.TryDownloadData(imgUrl, out var data))
                    yield return data;
            }
        }

        public IEnumerable<string> GetImgUrlIter(string query)
        {
            var offset = 0;
            while (true)
            {
                var url = UrlHelper.AddParameters(GoogleApiUrl, new Dictionary<string, string>
                {
                    ["key"] = _key,
                    ["cx"] = _cx,
                    ["q"] = query,
                    ["searchType"] = "image",
                    ["start"] = offset.ToString()
                });

                using var webClient = new WebClient();
                var response = webClient.DownloadString(url);
                var jObj = JsonConvert.DeserializeObject<JObject>(response);
                var imgUrls = jObj["items"].Select(item => item["link"].ToString()).ToArray();
                foreach (var imgUrl in imgUrls)
                    yield return imgUrl;
                offset += imgUrls.Length;
            }
        }

        public IAsyncEnumerator<BitmapImage> GetAsyncEnumerator(string query)
        {
            return new GoogleImagesAsyncEnumerator(_cx, _key, query);
        }

    }
}
