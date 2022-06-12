using Newtonsoft.Json.Linq;
using Root.Exceptions;
using Root.Services.Abstract;

namespace Root.Settings
{
    public abstract class BaseSettings : ISettings
    {
        private readonly string _settingsPath;

        private readonly ISettingsJsonLoader _settingsJsonLoader;

        protected BaseSettings(
            string settingsPath,
            ISettingsJsonLoader settingsJsonLoader)
        {
            _settingsPath = settingsPath;
            _settingsJsonLoader = settingsJsonLoader;

            Load();
        }

        public void Load()
        {
            JObject jSettings;
            try
            {
                jSettings = _settingsJsonLoader.Load(_settingsPath);
            }
            catch (SettingsIOException e)
            {
                throw new SettingsLoadException($"Error on load settings at path \"{_settingsPath}\".", e);
            }

            SetSettingsFromJObject(jSettings);
        }

        public void Save()
        {
            var jSettings = GetSettingsAsJObject();
            try
            {
                _settingsJsonLoader.Save(_settingsPath, jSettings);
            }
            catch (SettingsIOException e)
            {
                throw new SettingsSaveException($"Error on save settings at path \"{_settingsPath}\".", e);
            }
        }

        /// <exception cref="SettingsLoadException">Error on extract values from json object.</exception>
        protected abstract void SetSettingsFromJObject(JObject jSettings);

        protected abstract JObject GetSettingsAsJObject();
    }
}