using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Enums;
using Module.MusicSourcesStorage.Logic.Exceptions;
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
        using var archive = OpenArchive(filePath);

        return archive.Entries
            .Where(x => !x.IsDirectory)
            .Select(CreateModel)
            .ToList();
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