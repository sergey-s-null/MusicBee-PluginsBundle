using System.Data.Entity;
using Module.Settings.Database.Models;
using Module.Settings.Database.Services.Abstract;

namespace Module.Settings.Database.Services;

public sealed class SettingsRepository : ISettingsRepository
{
    private readonly Func<SettingsContext> _contextFactory;

    public SettingsRepository(Func<SettingsContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public string? FindString(string area, string id)
    {
        using var context = _contextFactory();

        var setting = context.Settings
            .Include(x => x.Value)
            .FirstOrDefault(x => x.Area == area && x.Id == id);

        return setting?.Value is StringSettingValue stringValue
            ? stringValue.Value
            : null;
    }

    public int? FindInt32(string area, string id)
    {
        using var context = _contextFactory();

        var setting = context.Settings
            .Include(x => x.Value)
            .FirstOrDefault(x => x.Area == area && x.Id == id);

        return setting?.Value is Int32SettingValue int32Value
            ? int32Value.Value
            : null;
    }

    public void Set(string area, string id, string value)
    {
        using var context = _contextFactory();

        var setting = context.Settings
            .Include(x => x.Value)
            .FirstOrDefault(x => x.Area == area && x.Id == id);

        if (setting is not null)
        {
            setting.Value = new StringSettingValue { Value = value };
        }
        else
        {
            context.Settings.Add(new SettingEntry
            {
                Area = area,
                Id = id,
                Value = new StringSettingValue { Value = value }
            });
        }

        context.SaveChanges();
    }

    public void Set(string area, string id, int value)
    {
        using var context = _contextFactory();

        var setting = context.Settings
            .Include(x => x.Value)
            .FirstOrDefault(x => x.Area == area && x.Id == id);

        if (setting is not null)
        {
            setting.Value = new Int32SettingValue { Value = value };
        }
        else
        {
            context.Settings.Add(new SettingEntry
            {
                Area = area,
                Id = id,
                Value = new Int32SettingValue { Value = value }
            });
        }

        context.SaveChanges();
    }
}