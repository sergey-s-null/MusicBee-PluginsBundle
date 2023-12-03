using Module.Settings.Logic.Exceptions;

namespace Module.Settings.Logic.Entities.Abstract;

public interface ISettings
{
    /// <exception cref="SettingsLoadException">Error on load settings.</exception>
    void Load();

    /// <exception cref="SettingsSaveException">Error on save settings.</exception>
    void Save();
}