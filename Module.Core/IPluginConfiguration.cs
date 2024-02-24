namespace Module.Core;

public interface IPluginConfiguration
{
    string DataSource { get; }

    // todo rename
    string PersistentStoragePath { get; }
}