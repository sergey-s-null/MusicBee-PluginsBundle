using Root.Exceptions;

namespace Root.GUI.AbstractViewModels
{
    public interface IBaseSettingsVM
    {
        bool Loaded { get; }

        void Load();

        /// <exception cref="SettingsSaveException">Invalid value of accessToken.</exception>
        void Save();
    }
}