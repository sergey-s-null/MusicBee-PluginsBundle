using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Module.ArtworksSearcher.Exceptions;
using Module.ArtworksSearcher.Helpers;
using Module.ArtworksSearcher.Services.Abstract;
using Module.ArtworksSearcher.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Root.Helpers;

namespace Module.ArtworksSearcher.Services
{
    public sealed class GoogleImageSearchService : IGoogleImageSearchService
    {
        private const string GoogleApiUrl = "https://www.googleapis.com/customsearch/v1";

        private readonly IArtworksSearcherSettings _artworksSearcherSettings;

        public GoogleImageSearchService(IArtworksSearcherSettings artworksSearcherSettings)
        {
            _artworksSearcherSettings = artworksSearcherSettings;
        }

        public async Task<IReadOnlyCollection<string>> SearchAsync(
            string query,
            int offset,
            CancellationToken cancellationToken)
        {
            var response = await GetGoogleResponseAsync(query, offset, cancellationToken);

            try
            {
                var jObject = JsonConvert.DeserializeObject<JObject>(response);
                if (jObject is null)
                {
                    throw new GoogleSearchImageException("Could not parse received data.");
                }

                return GetImageUrls(jObject);
            }
            catch (JsonException e)
            {
                throw new GoogleSearchImageException("Error on parse json data.", e);
            }
        }

        private async Task<string> GetGoogleResponseAsync(string query, int offset, CancellationToken cancellationToken)
        {
            var url = GetPreparedUrl(query, offset);

            using var webClient = new WebClient();
            using var tokenRegistration = cancellationToken.Register(webClient.CancelAsync);

            return await GetGoogleResponseAsync(webClient, url);
        }

        private string GetPreparedUrl(string query, int offset)
        {
            return UrlHelper.AddParameters(GoogleApiUrl, new Dictionary<string, string>
            {
                ["q"] = query,
                ["key"] = _artworksSearcherSettings.GoogleKey,
                ["cx"] = _artworksSearcherSettings.GoogleCX,
                ["searchType"] = "image",
                ["start"] = offset.ToString()
            });
        }

        private static async Task<string> GetGoogleResponseAsync(WebClient webClient, string url)
        {
            try
            {
                var response = await webClient.DownloadStringTaskAsync(url);
                if (response is null)
                {
                    throw new GoogleSearchImageException("Got empty response (null) from google web request.");
                }

                return response;
            }
            catch (WebException e)
            {
                throw new GoogleSearchImageException("Web error during request to google service.", e);
            }
        }

        private static IReadOnlyCollection<string> GetImageUrls(JObject responseJObject)
        {
            var itemsJArray = responseJObject.Value<JArray>("items");
            if (itemsJArray is null)
            {
                throw new GoogleSearchImageException("Got invalid data from google. \"items\" is empty.");
            }

            var imageUrls = itemsJArray
                .Select(item => item.Value<string>("link"))
                .Where(x => x is not null)
                .Select(x => x!)
                .ToReadOnlyCollection();

            return imageUrls;
        }
    }
}