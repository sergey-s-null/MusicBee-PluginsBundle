using Root.Abstractions;

namespace MusicBeePlugin.GUI.SettingsDialog
{
    public interface IModuleSettingsVM
    {
        public string ModuleName { get; }
        public ISettings ModuleSettings { get; }
    }
}