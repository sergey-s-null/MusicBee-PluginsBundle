using System.Windows.Input;
using Module.Settings.Exceptions;

namespace Module.Settings.Gui.AbstractViewModels;

public interface IBaseSettingsVM
{
    ICommand ReloadCmd { get; }

    bool Loaded { get; }

    // todo nullable?
    string LoadingErrorMessage { get; }

    // todo only command is available in VMs
    void Load();

    /// <returns>true - settings saved successfully. false - user-side error occurred, saving cancelled.</returns>
    /// <exception cref="SettingsSaveException">Invalid value of accessToken.</exception>
    bool Save();
}