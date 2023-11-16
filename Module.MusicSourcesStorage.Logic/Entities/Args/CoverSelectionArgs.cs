namespace Module.MusicSourcesStorage.Logic.Entities.Args;

public abstract record CoverSelectionArgs(
    bool SkipImageDownloadingIfDownloaded
);