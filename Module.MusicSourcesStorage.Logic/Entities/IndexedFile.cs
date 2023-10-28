using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed record IndexedFile(
    string Path,
    long Size,
    FileType Type
);