using System.Collections.Generic;
using System.Windows.Input;
using Root.Abstractions;

namespace MusicBeePlugin.GUI.SettingsDialog
{
    public interface ISettingsDialogVM : ISettings
    {
        IList<IModuleSettingsVM> Settings { get; }
        IModuleSettingsVM SelectedSettingsModule { get; set; }
        ICommand ResetCmd { get; }
    }
}