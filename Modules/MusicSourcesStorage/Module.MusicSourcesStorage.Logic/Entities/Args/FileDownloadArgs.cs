namespace Module.MusicSourcesStorage.Logic.Entities.Args;

public sealed record FileDownloadArgs(
    bool SkipIfDownloaded
);