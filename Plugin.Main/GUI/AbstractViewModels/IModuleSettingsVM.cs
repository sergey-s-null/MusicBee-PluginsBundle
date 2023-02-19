using Module.Settings.Gui.AbstractViewModels;

namespace Plugin.Main.GUI.AbstractViewModels
{
    public interface IModuleSettingsVM
    {
        public string ModuleName { get; }
        public IBaseSettingsVM ModuleSettings { get; }
    }
}