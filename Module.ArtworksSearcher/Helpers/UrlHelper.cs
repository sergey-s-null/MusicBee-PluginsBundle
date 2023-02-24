using System.Text;

namespace Module.ArtworksSearcher.Helpers;

public static class UrlHelper
{
    public static string AddParameters(string url, Dictionary<string, string> parameters)
    {
        var builder = new StringBuilder();
        builder.Append(url);
        if (url.Length > 0 && url[url.Length - 1] != '?')
            builder.Append('?');
        var first = true;
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