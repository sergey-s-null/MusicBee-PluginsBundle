﻿using Module.Core.Helpers;
using Module.Core.Services.Abstract;
using Module.Settings.Logic.Entities.Abstract;
using Module.Settings.Logic.Exceptions;
using Module.Settings.Logic.Services.Abstract;
using MoreLinq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Module.PlaylistsExporter.Settings;

public sealed class PlaylistsExporterSettings : BaseSettings, IPlaylistsExporterSettings
{
    public string PlaylistsDirectoryPath { get; set; } = "";
    public string FilesLibraryPath { get; set; } = "";
    public string PlaylistsNewDirectoryName { get; set; } = "";
    public IReadOnlyCollection<string> PlaylistsForExport { get; set; } = Array.Empty<string>();

    public PlaylistsExporterSettings(
        ISettingsFiles settingsFiles,
        IJsonLoader jsonLoader)
        : base(settingsFiles.PlaylistExporterSettingsFilePath, jsonLoader)
    {
    }

    protected override void SetDefaultSettings()
    {
        PlaylistsDirectoryPath = string.Empty;
        FilesLibraryPath = string.Empty;
        PlaylistsNewDirectoryName = string.Empty;
        PlaylistsForExport = Array.Empty<string>();
    }

    protected override void SetSettingsFromJObject(JObject rootObj)
    {
        try
        {
            PlaylistsDirectoryPath = rootObj.Value<string>(nameof(PlaylistsDirectoryPath)) ?? string.Empty;
            FilesLibraryPath = rootObj.Value<string>(nameof(FilesLibraryPath)) ?? string.Empty;
            PlaylistsNewDirectoryName = rootObj.Value<string>(nameof(PlaylistsNewDirectoryName)) ?? string.Empty;
            PlaylistsForExport = rootObj.Value<JArray>(nameof(PlaylistsForExport))?
                                     .Values<string>()
                                     .WhereNotNull()
                                     .ToReadOnlyCollection()
                                 ?? Array.Empty<string>();
        }
        catch (JsonException e)
        {
            throw new SettingsLoadException("Error on set settings from json object.", e);
        }
    }

    protected override JObject GetSettingsAsJObject()
    {
        var playlistsForExportJArray = new JArray();
        PlaylistsForExport.ForEach(x => playlistsForExportJArray.Add(x));

        return new JObject
        {
            [nameof(PlaylistsDirectoryPath)] = PlaylistsDirectoryPath,
            [nameof(FilesLibraryPath)] = FilesLibraryPath,
            [nameof(PlaylistsNewDirectoryName)] = PlaylistsNewDirectoryName,
            [nameof(PlaylistsForExport)] = playlistsForExportJArray,
        };
    }
}