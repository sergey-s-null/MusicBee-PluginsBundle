using Root.Abstractions;

namespace MusicBeePlugin.GUI.SettingsDialog
{
    public class ModuleSettingsVM
    {
        public string ModuleName { get; }
        public ISettings ModuleSettings { get; }
        
        public ModuleSettingsVM(string moduleName, ISettings moduleSettings)
        {
            ModuleName = moduleName;
            ModuleSettings = moduleSettings;
        }
    }
}