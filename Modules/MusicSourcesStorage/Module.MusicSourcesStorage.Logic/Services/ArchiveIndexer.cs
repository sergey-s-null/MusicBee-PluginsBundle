using Module.MusicSourcesStorage.Logic.Delegates;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;
using Module.MusicSourcesStorage.Logic.Enums;
using Module.MusicSourcesStorage.Logic.Exceptions;
using Module.MusicSourcesStorage.Logic.Factories;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using SharpCompress.Archives.Zip;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class ArchiveIndexer : IArchiveIndexer
{
    private readonly IFileClassifier _fileClassifier;

    public ArchiveIndexer(IFileClassifier fileClassifier)
    {
        _fileClassifier = fileClassifier;
    }

    public IReadOnlyList<SourceFile> Index(string filePath)
    {
        return Index(filePath, null, default);
    }

    public IActivableTask<string, IReadOnlyList<SourceFile>> CreateIndexingTask()
    {
        return ActivableTaskFactory.Create<string, IReadOnlyList<SourceFile>>(Index);
    }

    private IReadOnlyList<SourceFile> Index(
        string filePath,
        RelativeProgressCallback? progressCallback,
        CancellationToken token)
    {
        using var archive = OpenArchive(filePath);

        token.ThrowIfCancellationRequested();

        var files = archive.Entries
            .Where(x => !x.IsDirectory)
            .ToList();

        var result = new List<SourceFile>(files.Count);
        progressCallback?.Invoke(0);
        for (var i = 0; i < files.Count; i++)
        {
            result.Add(CreateModel(files[i]));
            progressCallback?.Invoke((double)i / files.Count);
            token.ThrowIfCancellationRequested();
        }

        return result;
    }

    private SourceFile CreateModel(ZipArchiveEntry zipArchiveEntry)
    {
        var fileType = _fileClassifier.Classify(zipArchiveEntry.Key);

        return fileType switch
        {
            FileType.MusicFile => MusicFile.New(zipArchiveEntry.Key, zipArchiveEntry.Size),
            FileType.Image => ImageFile.New(zipArchiveEntry.Key, zipArchiveEntry.Size),
            FileType.Unknown => UnknownFile.New(zipArchiveEntry.Key, zipArchiveEntry.Size),
            _ => throw new ArgumentOutOfRangeException(nameof(fileType), fileType, "Unknown file type.")
        };
    }

    private static ZipArchive OpenArchive(string filePath)
    {
        try
        {
            return ZipArchive.Open(filePath);
        }
        catch (Exception e)
        {
            throw new ArchiveIndexingException($"Error on open archive \"{filePath}\".", e);
        }
    }
}