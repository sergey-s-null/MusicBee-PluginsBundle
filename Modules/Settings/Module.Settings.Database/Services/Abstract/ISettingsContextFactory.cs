namespace Module.Settings.Database.Services.Abstract;

public interface ISettingsContextFactory
{
    SettingsContext Create();
}