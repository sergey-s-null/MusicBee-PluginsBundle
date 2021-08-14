using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworksSearcher.Ex
{
    public static class UrlEx
    {
        public static string AddParameters(string url, Dictionary<string, string> parameters)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(url);
            if (url.Length > 0 && url[url.Length - 1] != '?')
                builder.Append('?');
            bool first = true;
            foreach (var pair in parameters)
            {
                if (!first)
                    builder.Append('&');
                else
                    first = false;
                builder.Append(pair.Key);
                builder.Append("=");
                builder.Append(pair.Value);
            }
            return builder.ToString();
        }
    }
}
