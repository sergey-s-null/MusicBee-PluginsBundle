using System.Net;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Exceptions;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class VkDocumentDownloader : IVkDocumentDownloader
{
    private readonly IConfiguration _configuration;

    public VkDocumentDownloader(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> DownloadAsync(VkDocument document, CancellationToken token)
    {
        using var client = new WebClient();
        using var _ = token.Register(client.CancelAsync);

        var uri = GetDownloadUri(document.Uri);
        var filePath = Path.Combine(_configuration.VkDocumentsDownloadingDirectory, document.Name);

        Directory.CreateDirectory(_configuration.VkDocumentsDownloadingDirectory);

        await client.DownloadFileTaskAsync(uri, filePath);

        return filePath;
    }

    private static Uri GetDownloadUri(string uri)
    {
        try
        {
            return new Uri(uri);
        }
        catch (UriFormatException e)
        {
            throw new VkDocumentDownloadingException("Error on create Uri.", e);
        }
    }
}