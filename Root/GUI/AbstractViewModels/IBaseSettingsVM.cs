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

        /// <exception cref="SettingsSaveException">Invalid value of accessToken.</exception>
        void Save();
    }
}