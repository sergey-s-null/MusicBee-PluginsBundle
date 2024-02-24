using Module.Core.Services.Abstract;
using Module.Settings.Logic.Entities.Abstract;
using Module.Settings.Logic.Exceptions;
using Module.Settings.Logic.Services.Abstract;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Module.VkAudioDownloader.Settings;

public sealed class MusicDownloaderSettings : BaseSettings, IMusicDownloaderSettings
{
    public string DownloadDirTemplate { get; set; } = "";
    public string FileNameTemplate { get; set; } = "";

    public MusicDownloaderSettings(
        ISettingsFiles settingsFiles,
        IJsonLoader jsonLoader)
        : base(settingsFiles.AudioDownloaderSettingsFilePath, jsonLoader)
    {
    }

    protected override void SetSettingsFromJObject(JObject rootObj)
    {
        try
        {
            DownloadDirTemplate = rootObj.Value<string>(nameof(DownloadDirTemplate)) ?? "";
            FileNameTemplate = rootObj.Value<string>(nameof(FileNameTemplate)) ?? "";
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
            [nameof(DownloadDirTemplate)] = DownloadDirTemplate,
            [nameof(FileNameTemplate)] = FileNameTemplate
        };
    }
}