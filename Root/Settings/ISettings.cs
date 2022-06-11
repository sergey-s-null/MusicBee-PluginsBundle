using Root.Exceptions;

namespace Root.Settings
{
    public interface ISettings
    {
        /// <exception cref="SettingsLoadException">Error on load settings.</exception>
        void Load();

        /// <exception cref="SettingsSaveException">Error on save settings.</exception>
        void Save();
    }
}