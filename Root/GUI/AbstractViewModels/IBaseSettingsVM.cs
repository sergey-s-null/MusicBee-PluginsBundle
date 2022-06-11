namespace Root.GUI.AbstractViewModels
{
    public interface IBaseSettingsVM
    {
        // todo with exceptions?
        bool Load();
        bool Save();
    }
}