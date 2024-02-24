using Module.Settings.Logic.Exceptions;
using Module.Settings.Logic.Services.Abstract;
using Newtonsoft.Json.Linq;

namespace Module.Settings.Logic.Entities.Abstract;

public abstract class BaseSettings : ISettings
{
    private readonly string _settingsFilePath;

    private readonly IJsonLoader _jsonLoader;

    protected BaseSettings(
        string settingsFilePath,
        IJsonLoader jsonLoader)
    {
        _settingsFilePath = settingsFilePath;
        _jsonLoader = jsonLoader;

        Load();
    }

    public void Load()
    {
        JObject jSettings;
        try
        {
            jSettings = _jsonLoader.Load(_settingsFilePath);
        }
        catch (SettingsIOException e)
        {
            throw new SettingsLoadException($"Error on load settings at path \"{_settingsFilePath}\".", e);
        }

        SetSettingsFromJObject(jSettings);
    }

    public void Save()
    {
        var jSettings = GetSettingsAsJObject();
        try
        {
            _jsonLoader.Save(_settingsFilePath, jSettings);
        }
        catch (SettingsIOException e)
        {
            throw new SettingsSaveException($"Error on save settings at path \"{_settingsFilePath}\".", e);
        }
    }

    /// <exception cref="SettingsLoadException">Error on extract values from json object.</exception>
    protected abstract void SetSettingsFromJObject(JObject jSettings);

    protected abstract JObject GetSettingsAsJObject();
}