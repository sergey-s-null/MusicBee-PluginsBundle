namespace Module.MusicSourcesStorage.Logic.Entities;

/// <param name="ArchiveFilePath">Path to archive with file to be extracted.</param>
/// <param name="FilePathInArchive">Relative path of extracting file inside archive.</param>
/// <param name="TargetFilePath">Path to file in which extracting file should be saved.</param>
/// <param name="CreateDirectory">Create directory in which target file should be located.</param>
public sealed record FileExtractionArgs(
    string ArchiveFilePath,
    string FilePathInArchive,
    string TargetFilePath,
    bool CreateDirectory
);