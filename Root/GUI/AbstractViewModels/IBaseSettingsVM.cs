using System.Windows.Input;
using Root.Exceptions;

namespace Root.GUI.AbstractViewModels
{
    public interface IBaseSettingsVM
    {
        ICommand ReloadCmd { get; }

        bool Loaded { get; }

        string LoadingErrorMessage { get; }

        void Load();

        /// <returns>true - settings saved successfully. false - user-side error occurred, saving cancelled.</returns>
        /// <exception cref="SettingsSaveException">Invalid value of accessToken.</exception>
        bool Save();
    }
}