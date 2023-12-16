using System;
using System.IO;
using Module.MusicSourcesStorage.Core;

namespace Debug.MusicSourcesStorage;

public sealed class DebugModuleConfiguration : IModuleConfiguration
{
    public static readonly string DebugFolder = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
        "DebugMusicSourcesStorage"
    );

    private static readonly string DatabaseFilePath = Path.Combine(DebugFolder, "MusicSourcesStorage.mdf");

    public string DatabaseConnectionString { get; } = "Data Source=(LocalDb)\\MusicBeePlugins-Debug;" +
                                                      "Initial Catalog=MusicSourcesStorage;" +
                                                      $"AttachDBFilename={DatabaseFilePath};";

    public string SettingsArea => "music-sources-storage";
}