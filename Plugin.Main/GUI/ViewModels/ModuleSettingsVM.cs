using MusicBeePlugin.GUI.AbstractViewModels;
using Root.GUI.AbstractViewModels;

namespace MusicBeePlugin.GUI.ViewModels
{
    public class ModuleSettingsVM : IModuleSettingsVM
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