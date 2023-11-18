using System.Net;
using Module.Core.Helpers;
using Module.MusicSourcesStorage.Core.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Delegates;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Tasks;
using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;
using Module.MusicSourcesStorage.Logic.Services.Abstract;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class VkDocumentDownloader : IVkDocumentDownloader
{
    private readonly IModuleConfiguration _configuration;

    public VkDocumentDownloader(IModuleConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActivableTask<VkDocument, string> CreateDownloadTask()
    {
        return new ActivableTask<VkDocument, string>(DownloadInternal);
    }

    private string DownloadInternal(
        VkDocument document,
        RelativeProgressCallback progressCallback,
        CancellationToken token)
    {
        progressCallback(0);
        var targetFilePath = GetTargetFilePath(document);
        DirectoryHelper.CreateForFile(targetFilePath);
        DownloadFile(document.Uri, targetFilePath, progressCallback, token);
        return targetFilePath;
    }

    private string GetTargetFilePath(VkDocument document)
    {
        var fileName = PathHelper.ReplaceInvalidChars(document.Name, "_");
        return Path.Combine(_configuration.VkDocumentsDownloadingDirectory, fileName);
    }

    private static void DownloadFile(
        string sourceUri,
        string targetFilePath,
        RelativeProgressCallback progressCallback,
        CancellationToken token)
    {
        using var webClient = new WebClient();
        token.Register(webClient.CancelAsync);
        webClient.DownloadProgressChanged += (_, args) => progressCallback(args.ProgressPercentage / 100.0);

        webClient.DownloadFile(sourceUri, targetFilePath);
    }
}