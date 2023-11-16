namespace Module.MusicSourcesStorage.Logic.Entities.Args;

/// <param name="Archive">Archive with required file.</param>
/// <param name="SourceFile">Source file to download.</param>
/// <param name="SkipIfDownloaded">Skip downloading if file is present.</param>
/// <param name="TargetFilePath">Path to file in which downloaded file should be saved.</param>
public sealed record VkArchiveFileDownloadArgs(
    VkDocument Archive,
    SourceFile SourceFile,
    bool SkipIfDownloaded,
    string TargetFilePath
);