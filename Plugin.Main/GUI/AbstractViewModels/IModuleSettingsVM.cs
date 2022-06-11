using Root.GUI.AbstractViewModels;

namespace MusicBeePlugin.GUI.AbstractViewModels
{
    public interface IModuleSettingsVM
    {
        public string ModuleName { get; }
        public IBaseSettingsVM ModuleSettings { get; }
    }
}