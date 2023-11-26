using System;
using System.IO;
using Mead.MusicBee.Enums;
using Module.MusicSourcesStorage.Core;

namespace Debug.MusicSourcesStorage;

// todo remove useless values
public sealed class DebugModuleConfiguration : IModuleConfiguration
{
    private static readonly string DebugFolder = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
        "DebugMusicSourcesStorage"
    );

    private static readonly string DatabaseFilePath = Path.Combine(DebugFolder, "MusicSourcesStorage.mdf");

    public string VkDocumentsDownloadingDirectory { get; } = Path.Combine(DebugFolder, "VkDocuments");

    public string SourceFilesDownloadingDirectory { get; } = Path.Combine(DebugFolder, "SourceFiles");

    public string DatabaseConnectionString { get; } = "Data Source=(LocalDb)\\MSSQLLocalDB;" +
                                                      "Initial Catalog=MusicSourcesStorage;" +
                                                      $"AttachDBFilename={DatabaseFilePath};";

    public int CoverSize => 100;

    public MetaDataType FileIdField => MetaDataType.Custom10;
}