namespace Module.Settings.Database.Services.Abstract;

public interface ISettingsRepository
{
    string? FindString(string area, string id);

    int? FindInt32(string area, string id);

    void Set(string area, string id, string value);

    void Set(string area, string id, int value);
}