using Module.Settings.Gui.AbstractViewModels;
using MusicBeePlugin.GUI.AbstractViewModels;

namespace MusicBeePlugin.GUI.ViewModels
{
    public sealed class ModuleSettingsVM : IModuleSettingsVM
    {
        public string ModuleName { get; }
        public IBaseSettingsVM ModuleSettings { get; }

        public ModuleSettingsVM(string moduleName, IBaseSettingsVM moduleSettings)
        {
            ModuleName = moduleName;
            ModuleSettings = moduleSettings;
        }
    }
}