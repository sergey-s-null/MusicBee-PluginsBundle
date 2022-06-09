using Root.Abstractions;

namespace MusicBeePlugin.GUI.AbstractViewModels
{
    public interface IModuleSettingsVM
    {
        public string ModuleName { get; }
        public ISettings ModuleSettings { get; }
    }
}