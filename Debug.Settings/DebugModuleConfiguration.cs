using System;
using System.IO;
using Module.Settings.Core;

namespace Debug.Settings;

public sealed class DebugModuleConfiguration : IModuleConfiguration
{
    private static readonly string DebugFolder = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
        "DebugSettings"
    );

    private static readonly string DatabaseFilePath = Path.Combine(DebugFolder, "Settings.mdf");

    public string DatabaseConnectionString { get; } = "Data Source=(LocalDb)\\MusicBeePlugins-Debug;" +
                                                      "Initial Catalog=Settings;" +
                                                      $"AttachDBFilename={DatabaseFilePath};";
}