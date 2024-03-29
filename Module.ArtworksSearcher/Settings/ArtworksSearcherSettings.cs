﻿using Module.Core.Services.Abstract;
using Module.Settings.Logic.Entities.Abstract;
using Module.Settings.Logic.Exceptions;
using Module.Settings.Logic.Services.Abstract;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Module.ArtworksSearcher.Settings;

public sealed class ArtworksSearcherSettings : BaseSettings, IArtworksSearcherSettings
{
    public string GoogleCX { get; set; } = "";
    public string GoogleKey { get; set; } = "";

    public int MaxParallelDownloadsCount => 10; // TODO from config

    private int _parallelDownloadsCount;

    public int ParallelDownloadsCount
    {
        get => _parallelDownloadsCount;
        set
        {
            if (value < 1)
            {
                _parallelDownloadsCount = 1;
            }
            else if (value > MaxParallelDownloadsCount)
            {
                _parallelDownloadsCount = MaxParallelDownloadsCount;
            }
            else
            {
                _parallelDownloadsCount = value;
            }
        }
    }

    public string OsuSongsDir { get; set; } = "";
    public long MinOsuImageByteSize { get; set; }

    public ArtworksSearcherSettings(
        ISettingsFiles settingsFiles,
        IJsonLoader jsonLoader)
        : base(settingsFiles.ArtworksSearcherSettingsFilePath, jsonLoader)
    {
    }

    protected override void SetDefaultSettings()
    {
        GoogleCX = string.Empty;
        GoogleKey = string.Empty;
        ParallelDownloadsCount = default;
        OsuSongsDir = string.Empty;
        MinOsuImageByteSize = default;
    }

    protected override void SetSettingsFromJObject(JObject rootObj)
    {
        try
        {
            GoogleCX = rootObj.Value<string>(nameof(GoogleCX)) ?? string.Empty;
            GoogleKey = rootObj.Value<string>(nameof(GoogleKey)) ?? string.Empty;
            ParallelDownloadsCount = rootObj.Value<int>(nameof(ParallelDownloadsCount));
            OsuSongsDir = rootObj.Value<string>(nameof(OsuSongsDir)) ?? string.Empty;
            MinOsuImageByteSize = rootObj.Value<long>(nameof(MinOsuImageByteSize));
        }
        catch (JsonException e)
        {
            throw new SettingsLoadException("Error on set settings from json object.", e);
        }
    }

    protected override JObject GetSettingsAsJObject()
    {
        return new JObject
        {
            [nameof(GoogleCX)] = GoogleCX,
            [nameof(GoogleKey)] = GoogleKey,
            [nameof(ParallelDownloadsCount)] = ParallelDownloadsCount,
            [nameof(OsuSongsDir)] = OsuSongsDir,
            [nameof(MinOsuImageByteSize)] = MinOsuImageByteSize
        };
    }
}