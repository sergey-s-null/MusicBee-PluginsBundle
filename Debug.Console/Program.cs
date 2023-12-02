using System.Net;
using System.Text.RegularExpressions;
using Debug.Common;
using VkNet.Model.RequestParams;

namespace Debug.Console;

internal static class Program
{
    private static readonly string M3U8FilePath = Path.GetFullPath(@"../../delete_this/index.m3u8");
    private static readonly string BaseUrlFilePath = Path.GetFullPath(@"../../delete_this/baseUrl.txt");

    static async Task Main(string[] args)
    {
    }

    // download m3u8 and baseUrl
    private static void Part1()
    {
        var api = VkHelper.GetAuthorizedVkApi();

        // TODO change indices
        var audio = api.Audio.Get(new AudioGetParams() { Offset = 1, Count = 1 })[0];

        using (var webClient = new WebClient())
        {
            var data = webClient.DownloadData(audio.Url);
            File.WriteAllBytes(M3U8FilePath, data);
        }

        var regex = new Regex(@"(^.*/)index\.m3u8");
        var match = regex.Match(audio.Url.AbsoluteUri);
        var baseUrl = match.Groups[1].Value;
        File.WriteAllText(BaseUrlFilePath, baseUrl);
    }
}