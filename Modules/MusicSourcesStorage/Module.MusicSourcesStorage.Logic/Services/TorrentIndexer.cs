using Module.MusicSourcesStorage.Logic.Delegates;
using Module.MusicSourcesStorage.Logic.Entities;
using Module.MusicSourcesStorage.Logic.Entities.Tasks.Abstract;
using Module.MusicSourcesStorage.Logic.Enums;
using Module.MusicSourcesStorage.Logic.Factories;
using Module.MusicSourcesStorage.Logic.Services.Abstract;
using MonoTorrent;

namespace Module.MusicSourcesStorage.Logic.Services;

public sealed class TorrentIndexer : ITorrentIndexer
{
    private readonly IFileClassifier _fileClassifier;

    public TorrentIndexer(IFileClassifier fileClassifier)
    {
        _fileClassifier = fileClassifier;
    }

    public IActivableTask<string, IReadOnlyList<SourceFile>> CreateIndexingTask()
    {
        return ActivableTaskFactory.Create<string, IReadOnlyList<SourceFile>>(Index);
    }

    private IReadOnlyList<SourceFile> Index(
        string torrentFilePath,
        RelativeProgressCallback? progressCallback,
        CancellationToken token)
    {
        var torrent = Torrent.Load(torrentFilePath);

        var result = new List<SourceFile>(torrent.Files.Count);
        progressCallback?.Invoke(0);
        for (var i = 0; i < torrent.Files.Count; i++)
        {
            token.ThrowIfCancellationRequested();
            result.Add(CreateSourceFile(torrent.Files[i]));
            progressCallback?.Invoke((double)(i + 1) / torrent.Files.Count);
        }

        return result;
    }

    private SourceFile CreateSourceFile(ITorrentFile torrentFile)
    {
        var fileType = _fileClassifier.Classify(torrentFile.Path);
        return CreateSourceFile(fileType, torrentFile);
    }

    private static SourceFile CreateSourceFile(FileType fileType, ITorrentFile torrentFile)
    {
        return fileType switch
        {
            FileType.MusicFile => MusicFile.New(torrentFile.Path, torrentFile.Length),
            FileType.Image => ImageFile.New(torrentFile.Path, torrentFile.Length),
            FileType.Unknown => UnknownFile.New(torrentFile.Path, torrentFile.Length),
            _ => throw new ArgumentOutOfRangeException(nameof(fileType), fileType, "Unsupported file type.")
        };
    }
}