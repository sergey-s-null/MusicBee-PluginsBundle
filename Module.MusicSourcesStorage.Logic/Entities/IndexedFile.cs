using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Logic.Entities;

[Obsolete("Use derived from FileModel classes")]
public sealed record IndexedFile(
    string Path,
    long Size,
    FileType Type
);