namespace Module.Settings.Database.Services.Abstract;

public interface ISettingsDbMigrator
{
    void UpdateToLatest();
}