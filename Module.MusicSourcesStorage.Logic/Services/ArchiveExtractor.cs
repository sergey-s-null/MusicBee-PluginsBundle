using Module.Core.Helpers;
using Module.MusicSourcesStorage.Logic.Delegates;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Args;
using Module.MusicSourcesStorage.Logic.Entities.Tasks;
using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;
using Module.MusicSourcesStorage.Logic.Exceptions;
using Module.MusicSourcesStorage.Logic.Extensions;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using SharpCompress.Archives;
using SharpCompress.Archives.Zip;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class ArchiveExtractor : IArchiveExtractor
{
    public IActivableTaskWithProgress<FileExtractionArgs, string> CreateFileExtractionTask()
    {
        return new ActivableTaskWithProgress<FileExtractionArgs, string>(
            (arg, progressCallback, internalToken) => ExtractInternal(
                arg.ArchiveFilePath,
                arg.FilePathInArchive,
                arg.TargetFilePath,
                arg.CreateDirectory,
                progressCallback,
                internalToken
            )
        );
    }

    private static string ExtractInternal(
        string archiveFilePath,
        string filePathInArchive,
        string targetFilePath,
        bool createDirectory,
        RelativeProgressCallback progressCallback,
        CancellationToken token)
    {
        if (createDirectory)
        {
            CreateDirectoryForFile(targetFilePath);
        }

        using var archive = ZipArchive.Open(archiveFilePath);

        var entry = GetEntry(archive, filePathInArchive);
        using var source = entry.OpenEntryStream();
        using var destination = File.OpenWrite(targetFilePath);

        var task = source.CopyToAsync(
            destination,
            81920,
            new ProgressCallbackConfiguration
            {
                AbsoluteProgressCallback = x => progressCallback((double)x / entry.Size)
            },
            token
        );
        task.Wait(token);

        return targetFilePath;
    }

    private static void CreateDirectoryForFile(string filePath)
    {
        var directoryPath = Path.GetDirectoryName(filePath);
        if (directoryPath is null)
        {
            return;
        }

        Directory.CreateDirectory(directoryPath);
    }

    private static IArchiveEntry GetEntry(IArchive archive, string filePathInArchive)
    {
        var unifiedFilePath = PathHelper.UnifyFilePath(filePathInArchive);
        var entry = archive.Entries
            .Where(x => !x.IsDirectory)
            .FirstOrDefault(x => PathHelper.UnifyFilePath(x.Key) == unifiedFilePath);

        if (entry is null)
        {
            throw new ArchiveExtractionException(
                $"Could not find archive entry with path \"{filePathInArchive}\"."
            );
        }

        return entry;
    }
}