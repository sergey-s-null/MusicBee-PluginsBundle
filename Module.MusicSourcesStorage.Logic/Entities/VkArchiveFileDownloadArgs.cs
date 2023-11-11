namespace Module.MusicSourcesStorage.Logic.Entities;

/// <param name="Archive">Archive with required file.</param>
/// <param name="SourceFile">Source file to download.</param>
/// <param name="TargetFilePath">Path to file in which downloaded file should be saved.</param>
public sealed record VkArchiveFileDownloadArgs(
    VkDocument Archive,
    SourceFile SourceFile,
    string TargetFilePath
);