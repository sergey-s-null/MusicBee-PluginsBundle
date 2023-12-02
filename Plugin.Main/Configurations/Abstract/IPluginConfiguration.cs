namespace Plugin.Main.Configurations.Abstract;

public interface IPluginConfiguration
{
    string DataSource { get; }
    string PersistentStoragePath { get; }
}