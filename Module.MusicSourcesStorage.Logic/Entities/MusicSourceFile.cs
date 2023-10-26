using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed record MusicSourceFile(
    string Path,
    long Size,
    FileType Type
);