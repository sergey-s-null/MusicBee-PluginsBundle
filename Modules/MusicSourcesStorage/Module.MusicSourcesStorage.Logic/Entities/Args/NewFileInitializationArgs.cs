namespace Module.MusicSourcesStorage.Logic.Entities.Args;

public sealed record NewFileInitializationArgs(
    int FileId,
    string FilePath
);